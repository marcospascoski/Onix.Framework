using System.Collections.Generic;

namespace Onix.Framework.Infra.Data.Interfaces
{
    public interface IPagedItems<T>
    {
        IEnumerable<T> Items { get; }
        IPaged Paged { get; }
        int TotalItems { get; }
        int PageCount { get; }
        bool LastPage { get; }
    }
}