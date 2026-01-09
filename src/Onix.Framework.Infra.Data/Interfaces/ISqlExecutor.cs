using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onix.Framework.Infra.Data.Interfaces
{
    public interface ISqlExecutor
    {
        int ExecuteSql(string query, params object[] parameters);
        Task<int> ExecuteSqlAsync(string query, params object[] parameters);
        IEnumerable<T> Select<T>(string query, params object[] parameters);
        Task<IEnumerable<T>> SelectAsync<T>(string query, params object[] parameters);
    }
}