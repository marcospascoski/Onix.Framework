using Onix.Framework.Notifications.Interfaces;
using Onix.Framework.Shared.Interfaces;
using System.Collections.Generic;

namespace Onix.Framework.Shared.Results
{
    public class RequestDataResult<T> : RequestResult, IRequestDataResult<T>
    {
        public T Data { get; set; }

        public RequestDataResult() { }

        public RequestDataResult(IEnumerable<INotification> notifications = null, T data = default)
            : base(notifications)
        {
            Data = data;
        }
    }
}
