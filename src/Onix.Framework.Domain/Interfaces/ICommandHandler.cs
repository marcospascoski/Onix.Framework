using System.Threading.Tasks;

namespace Onix.Framework.Domain.Interfaces
{
    public interface ICommandHandler<TParameter, TResult>
    {
        Task<TResult> HandleAsync(TParameter parameter);
    }
}
