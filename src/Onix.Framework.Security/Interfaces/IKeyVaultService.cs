using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onix.Framework.Security.Interfaces
{
    public interface IKeyVaultService
    {
        Task<string> ObterChaveMestraAsync();
    }
}
