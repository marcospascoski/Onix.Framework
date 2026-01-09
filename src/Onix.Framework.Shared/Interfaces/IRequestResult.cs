using Onix.Framework.Notifications.Interfaces;
using System.Collections.Generic;

namespace Onix.Framework.Shared.Interfaces
{
    public interface IRequestResult
    {
        bool Success { get; }
        IEnumerable<INotification> Notifications { get; }
    }
}
