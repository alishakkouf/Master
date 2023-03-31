﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Lila.Platform.Shared;
using Master.Data.Models;
using Master.Domain.MultyTenants;
using Master.Shared;
using Master.Shared.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Master.Data
{
    public class MasterDbContext : IdentityDbContext<UserAccount,UserRole,int>
    {
        private static readonly List<string> HardDeletedList = new List<string> { nameof(UserRole) };
         
        internal DbSet<Setting> Settings { get; set; }
        internal DbSet<Tenant> Tenants { get; set; }
        internal DbSet<AuditLog> AuditLogs { get; set; }

        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly int? _currentTenantId;
        private readonly bool _ignoreTenant;

        public MasterDbContext(DbContextOptions<MasterDbContext> options,
    IConfiguration configuration,
    ICurrentUserService currentUserService)
    : base(options)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;

            _currentTenantId = _currentUserService.GetTenantId();

            _ignoreTenant = _configuration[Constants.AppIgnoreTenantIdKey]?.ToLower() == "true";
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(b =>
            {
                b.Metadata.RemoveIndex(new[] { b.Property(r => r.NormalizedName).Metadata });
            });

            //builder.Entity<UserRole>()
            //    .HasIndex(x => new { x.NormalizedName, x.TenantId }, "RoleNameIndex")
            //    .IsUnique();

            //builder.Entity<UserAccount>()
            //    //.HasOne(x => x.Address)
            //    .WithOne(x => x.UserAccount);



            //builder.Entity<UserAccount>()
            //    .HasMany(x => x.UserRoles)
            //    .WithMany(x => x.UserAccounts)
            //    .UsingEntity<IdentityUserRole<int>>(
            //        r => r.HasOne<UserRole>().WithMany().HasForeignKey(x => x.RoleId),
            //        l => l.HasOne<UserAccount>().WithMany().HasForeignKey(x => x.UserId));

            builder.Entity<UserRole>()
                .HasMany(x => x.Claims)
                .WithOne()
                .HasForeignKey(x => x.RoleId);


            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }



        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var temporalAuditEntries = OnBeforeSaveChanges(_currentUserService.GetUserId());
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(temporalAuditEntries, _currentUserService.GetUserId());
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges(long? userId)
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged ||
                    entry.Entity is AuditLog)
                    continue;

                if (_currentTenantId != null && entry.Entity is IHaveTenant entityWithTenant)
                    FillTenantId(entityWithTenant, entry.State, _currentTenantId);

                if (entry.Entity is IAuditedEntity)
                    FillAuditedAndChangeDeleted(entry, userId);

                var auditEntry = GetAuditEntry(entry);

                if (auditEntry != null)
                    auditEntries.Add(auditEntry);
            }

            RemoveDuplicateWeakEntities(auditEntries);

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit(userId));
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        /// <summary>
        /// Generate AuditEntry depending on EntityEntry. If entity is marked with <see cref="AuditedAttribute"/> and
        /// its type name is included in the configuration array: "App:AuditedArray".
        /// </summary>
        private AuditEntry GetAuditEntry(EntityEntry entry)
        {
            if (!_configuration.GetSection(Constants.AppAuditedArrayKey).Exists())
                return null;

            var auditedArray = _configuration.GetSection(Constants.AppAuditedArrayKey).GetChildren().Select(x => x.Value).ToList();

            if (!auditedArray.Contains(entry.Entity.GetType().Name))
                return null;

            var attribute = entry.Entity.GetType()
                    .GetCustomAttributes(typeof(AuditedAttribute)).FirstOrDefault() as AuditedAttribute;

            var auditEntry = new AuditEntry(entry) { EntityName = entry.Entity.GetType().Name };

            if (attribute == null)
                return null;

            auditEntry.EntityKeyName = attribute.KeyName;

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntry.Operation = AuditOperationType.Create;
                    break;
                case EntityState.Deleted:
                    auditEntry.Operation = AuditOperationType.Delete;
                    break;
                default:
                    auditEntry.Operation = AuditOperationType.Update;
                    break;
            }

            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    // value will be generated by the database, get the value after saving
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }

            return auditEntry;
        }

        private void FillTenantId(IHaveTenant entityWithTenant, EntityState entityState, int? tenantId)
        {
            switch (entityState)
            {
                case EntityState.Added:
                    entityWithTenant.TenantId = tenantId;
                    break;
            }
        }

        private void FillAuditedAndChangeDeleted(EntityEntry entry, long? userId)
        {
            if (entry.Entity is IAuditedEntity audited)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        if (!HardDeletedList.Contains(entry.Entity.GetType().Name))
                        {
                            entry.State = EntityState.Modified;
                            foreach (var entryProperty in entry.Properties)
                            {
                                if (entryProperty.Metadata.Name != nameof(audited.IsDeleted) &&
                                    entryProperty.Metadata.Name != nameof(audited.ModifiedAt) &&
                                    entryProperty.Metadata.Name != nameof(audited.ModifierUserId))
                                    entryProperty.IsModified = false;
                            }

                            audited.ModifiedAt = DateTime.UtcNow;
                            audited.ModifierUserId = userId;
                            audited.IsDeleted = true;
                        }
                        break;
                    case EntityState.Modified:
                        audited.ModifiedAt = DateTime.UtcNow;
                        audited.ModifierUserId = userId;
                        Entry(audited).Property(p => p.CreatedAt).IsModified = false;
                        Entry(audited).Property(p => p.CreatorUserId).IsModified = false;
                        break;
                    case EntityState.Added:
                        audited.CreatedAt = DateTime.UtcNow;
                        audited.CreatorUserId = userId;
                        audited.IsDeleted = false;
                        break;
                }
            }
        }

        private void RemoveDuplicateWeakEntities(List<AuditEntry> auditEntries)
        {
            if (auditEntries.Count == 0)
                return;

            var addedWeakEntities = auditEntries.Where(x => x.KeyValues.Count > 1 && x.Entry.State == EntityState.Added).ToList();
            var deletedWeakEntities = auditEntries.Where(x => x.KeyValues.Count > 1 && x.Entry.State == EntityState.Deleted).ToList();

            if (addedWeakEntities.Count == 0 || deletedWeakEntities.Count == 0)
                return;

            foreach (var addedWeakEntity in addedWeakEntities)
            {
                var deletedWeakEntity = deletedWeakEntities.FirstOrDefault(x =>
                    x.EntityName == addedWeakEntity.EntityName && DictIdentical(x.KeyValues, addedWeakEntity.KeyValues)
                                                               && DictIdentical(x.OldValues, addedWeakEntity.NewValues));

                if (deletedWeakEntity != null)
                {
                    auditEntries.Remove(addedWeakEntity);
                    auditEntries.Remove(deletedWeakEntity);
                }
            }
        }

        private static bool DictIdentical(Dictionary<string, object> a, Dictionary<string, object> b)
        {
            return a.Count == b.Count &&
                a.Keys.All(k => b.ContainsKey(k) && object.Equals(a[k], b[k]));
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries, long? userId)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                AuditLogs.Add(auditEntry.ToAudit(userId));
            }

            return base.SaveChangesAsync();
        }
    }
}
