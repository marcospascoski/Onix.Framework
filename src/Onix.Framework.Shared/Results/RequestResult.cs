using Onix.Framework.Notifications.Interfaces;
using Onix.Framework.Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Onix.Framework.Shared.Results
{
    public class RequestResult(IEnumerable<INotification> notifications = null) : IRequestResult
    {
        public bool Success => !(Notifications?.Any(x => x.IsError)).GetValueOrDefault();
        public IEnumerable<INotification> Notifications { get; } = notifications ?? new List<INotification>();
    }
}
