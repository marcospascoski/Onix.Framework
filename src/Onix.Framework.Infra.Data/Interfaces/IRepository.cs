using Onix.Framework.Domain.Interfaces;

namespace Onix.Framework.Infra.Data.Interfaces
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {        
    }
}