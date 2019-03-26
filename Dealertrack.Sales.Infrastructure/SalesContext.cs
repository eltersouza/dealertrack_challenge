using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Dealertrack.Sales.Domain.SeedWork;
using Dealertrack.Sales.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dealertrack.Sales.Infrastructure
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        public const string DEFAULT_SCHEMA = "ordering";

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleStatus> SaleStatuses { get; set; }

        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SaleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SaleStatusEntityTypeConfiguration());

            SeedDb(modelBuilder);
        }

        private void SeedDb(ModelBuilder builder)
        {
            builder.Entity<SaleStatus>().HasData(
                new SaleStatus(1, "Paid"),
                new SaleStatus(2, "Pending"),
                new SaleStatus(3, "Cancelled")
            );
        }

        public async Task<bool> SaveEntititiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}