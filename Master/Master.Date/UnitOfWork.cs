using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lila.Platform.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Master.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly MasterDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(MasterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync(IsolationLevel? isolationLevel = null)
        {
            if (_transaction == null)
            {
                if (!isolationLevel.HasValue)
                {
                    _transaction = await _dbContext.Database.BeginTransactionAsync();
                }
                else
                {
                    _transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel.Value);
                }
            }
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
                DetachAllEntities();
            }
        }

        private async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private void DetachAllEntities()
        {
            var modifiedEntries = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            modifiedEntries.ForEach(entry => entry.State = EntityState.Detached);
        }
    }
}