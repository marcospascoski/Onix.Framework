namespace Onix.Framework.Shared.Interfaces
{
    public interface IRequestDataResult<T> : IRequestResult
    {
        T Data { get; set; }
        //void SetData(T data);
    }
}
