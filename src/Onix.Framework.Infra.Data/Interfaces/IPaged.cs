namespace Onix.Framework.Infra.Data.Interfaces
{
    public interface IPaged : IOrdered
    {
        int PageSize { get; set; }
        int CurrentPage { get; set; }
        int SkipItems { get; }
    }
}