namespace Onix.Framework.Infra.Data.Interfaces
{
    public interface IOrdered
    {
        string PropertyName { get; set; }
        bool IsDescending { get; set; }
    }
}