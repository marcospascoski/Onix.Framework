using Onix.Framework.Infra.Data.Interfaces;

namespace Onix.Framework.Infra.Data.Implementation
{
    public class Paged : Ordered, IPaged
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int SkipItems => CurrentPage * PageSize;

        protected Paged() { }

        public Paged(string propertyName, bool isDescending, int pageSize, int currentPage)
            : base(propertyName, isDescending)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}