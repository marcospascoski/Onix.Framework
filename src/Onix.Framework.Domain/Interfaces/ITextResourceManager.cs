using System.Collections.Generic;

namespace Onix.Framework.Domain.Interfaces
{
    public interface ITextResourceManager
    {
        string GetString(string[] names);
        string GetString(string name, params object[] arguments);
        IEnumerable<string> GetAllNames();
    }
}
