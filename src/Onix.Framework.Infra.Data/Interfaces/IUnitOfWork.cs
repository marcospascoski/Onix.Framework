using Onix.Framework.Domain.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace Onix.Framework.Infra.Data.Interfaces
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IDbConnection Connection();
        IDbTransaction BeginTransaction();
        IDbTransaction GetTransaction();
        void RollBack();
        void Untrack<TEntity>(TEntity entity) where TEntity : class, IEntity;
    }
}