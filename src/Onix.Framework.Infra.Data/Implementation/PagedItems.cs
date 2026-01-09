using Onix.Framework.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace Onix.Framework.Infra.Data.Implementation
{
    public struct PagedItems<T> : IPagedItems<T>
    {
        public IEnumerable<T> Items { get; }
        public IPaged Paged { get; }
        public int TotalItems { get; }
        public int PageCount { get; }
        public bool LastPage => Paged.CurrentPage == PageCount - 1;
        
        public PagedItems(IPaged paged, IEnumerable<T> items, int totalItems)
        {
            Paged = paged;
            Items = items;
            TotalItems = totalItems;
            PageCount = (int)Math.Ceiling((decimal)TotalItems / (decimal)Paged.PageSize);
        }
    }
}