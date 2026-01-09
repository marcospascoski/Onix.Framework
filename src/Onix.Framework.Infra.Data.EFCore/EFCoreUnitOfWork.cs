using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Onix.Framework.Infra.Data.Interfaces;
using Onix.Framework.Domain.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace Onix.Framework.Infra.Data.EFCore
{
    public class EFCoreUnitOfWork(DbContext context) : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        public DbContext Context { get; private set; } = context;

        public int Commit()
        {
            _transaction?.Commit();
            _transaction = null;
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            _transaction?.Commit();
            _transaction = null;
            return await Context.SaveChangesAsync();
        }

        public IDbConnection Connection()
        {
            return Context.Database.GetDbConnection();
        }

        public IDbTransaction BeginTransaction()
        {
            _transaction = Context.Database.BeginTransaction();
            return _transaction.GetDbTransaction();
        }

        public IDbTransaction GetTransaction()
        {
            return _transaction?.GetDbTransaction();
        }

        public void RollBack()
        {
            _transaction?.Rollback();
            Context.ChangeTracker.Clear();
        }

        public void Untrack<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}