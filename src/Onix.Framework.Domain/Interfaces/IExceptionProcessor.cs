using System;
using System.Threading.Tasks;

namespace Onix.Framework.Domain.Interfaces
{
    public interface IExceptionProcessor
    {
        void Salvar(Exception exception);
        Task SalvarAsync(Exception exception);
    }
}
