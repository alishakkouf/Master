using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Master.Data.Models;
using Master.Data.Models.Account;
using Master.Data.Models.Role;
using Master.Domain.Authorization;
using Master.Domain.Settings;
using Master.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Master.Data
{
    internal static class MasterDBContextSeed
    {
        /// <summary>
        /// Seed default and first tenant with id = 1
        /// </summary>
        public static async Task SeedDefaultTenantAsync(MasterDbContext context)
        {
            if (!context.Tenants.Any(x => x.Id == Constants.DefaultTenantId))
            {
                await context.Database.OpenConnectionAsync();
                try
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Tenants ON");

                    var tenant = new Tenant
                    {
                        Id = Constants.DefaultTenantId,
                        Name = "Test",
                        AdminEmail = Constants.DefaultTenantAdmin,
                        DomainName = Constants.DefaultTenantDomain,
                        IsActive = true,
                        IsDeleted = false
                    };

                    await context.Tenants.AddAsync(tenant);
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Tenants OFF");
                }
                finally
                {
                    await context.Database.CloseConnectionAsync();
                }
            }
        }

        /// <summary> 
        /// Seed super admin user.
        /// </summary>
        internal static async Task SeedSuperAdminAsync(MasterDbContext context, RoleManager<UserRole> roleManager, UserManager<UserAccount> userManager)
        { 
            var role = await context.Roles.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == Constants.SuperAdminRoleName);
            if (role == null)
            {
                role = new UserRole(Constants.SuperAdminRoleName) { IsActive = true, TenantId = null ,Description = ""};
                await roleManager.CreateAsync(role);
            }

            var user = await context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.UserName.StartsWith(Constants.SuperAdminUserName));
            if (user == null)
            {
                user = new UserAccount
                {
                    UserName = Constants.SuperAdminEmail,
                    Email = Constants.SuperAdminEmail,
                    FirstName = "Super Admin",
                    LastName = "Super Admin",
                    PhoneNumber = "0123456789",
                    IsActive = true,
                    //TenantId = null
                };

                await userManager.CreateAsync(user, Constants.DefaultPassword);
                await userManager.AddToRolesAsync(user, new[] { Constants.SuperAdminRoleName });
            }
        }

        /// <summary>
        /// Seed static roles and add permissions claims to them.
        /// </summary>
        internal static async Task SeedStaticRolesAsync(RoleManager<UserRole> roleManager/*, Tenant tenant*/)
        {
            foreach (var rolePermission in StaticRolePermissions.Roles)
            {
                var role = await roleManager.Roles/*.IgnoreQueryFilters()*/.FirstOrDefaultAsync(x =>
                    x.NormalizedName == rolePermission.Key.ToUpper() /*&& x.TenantId == tenant.Id*/);

                if (role == null)
                {
                    role = new UserRole(rolePermission.Key) { IsActive = true, Description = ""/*, TenantId = tenant.Id*/ };

                    await roleManager.CreateAsync(role);

                    //Add static role permissions to db
                    foreach (var permission in rolePermission.Value)
                    {
                        await roleManager.AddClaimAsync(role,
                            new Claim(Constants.PermissionsClaimType, permission));
                    }

                    continue;
                }

                if (rolePermission.Key == StaticRoleNames.Administrator)
                {
                    var dbRoleClaims = await roleManager.GetClaimsAsync(role);

                    //Remove any claim in db and not in static role permissions.
                    foreach (var dbPermission in dbRoleClaims.Where(x => x.Type == Constants.PermissionsClaimType &&
                                                                         !rolePermission.Value.Contains(x.Value)))
                    {
                        await roleManager.RemoveClaimAsync(role, dbPermission);
                    }

                    //Add static role permissions to db if they don't already exist.
                    foreach (var permission in rolePermission.Value)
                    {
                        if (!dbRoleClaims.Any(x => x.Type == Constants.PermissionsClaimType && x.Value == permission))
                        {
                            await roleManager.AddClaimAsync(role,
                                new Claim(Constants.PermissionsClaimType, permission));
                        }
                    }
                }
            }
        }

        internal static async Task SeedDefaultUserAsync(UserManager<UserAccount> userManager, RoleManager<UserRole> roleManager/*, Tenant tenant*/)
        {
            var adminRole = await roleManager.Roles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Name == StaticRoleNames.Administrator/* && x.TenantId == tenant.Id*/);

            var adminUser = await userManager.Users.FirstOrDefaultAsync();

            if (adminUser == null && adminRole != null)
            {
                adminUser = new UserAccount 
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "0123456789",
                    IsActive = true
                };
                adminUser.UserRoles.Add(adminRole);
                await userManager.CreateAsync(adminUser, Constants.DefaultPassword);
            }
        }

        internal static async Task SeedDefaultSettingsAsync(MasterDbContext context/*, Tenant tenant*/)
        {
            var save = false;
            foreach (var setting in SettingDefaults.Defaults)
            {
                if (!context.Settings.IgnoreQueryFilters() 
                    .Any(x => x.Name == setting.Key && x.UserId == null /*&& x.TenantId == tenant.Id*/))
                {
                    await context.Settings.AddAsync(new Setting { Name = setting.Key/*, Value = setting.Value, UserId = null, TenantId = tenant.Id*/ });
                    save = true;
                }
            } 

            if (save) 
                await context.SaveChangesAsync();
        }
    }
}
