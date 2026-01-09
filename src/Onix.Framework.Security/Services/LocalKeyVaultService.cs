using Onix.Framework.Security.Interfaces;
using System.Threading.Tasks;

namespace Onix.Framework.Security.Services
{
    public class LocalKeyVaultService : IKeyVaultService
    {
        private readonly string _chaveMestra;

        public LocalKeyVaultService(string chaveMestra)
        {
            _chaveMestra = chaveMestra;
        }

        public Task<string> ObterChaveMestraAsync()
        {
            return Task.FromResult(_chaveMestra);
        }
    }
}