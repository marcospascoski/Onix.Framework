using Onix.Framework.Notifications.Enums;
using System.Collections.Generic;

namespace Onix.Framework.Notifications.Interfaces
{
    public interface INotificationContext
    {
        bool Success => !HasErrors;
        bool HasErrors { get; }
        IEnumerable<INotification> GetNotifications();
        IEnumerable<INotification> GetErrors();
        void Add(INotification notification);
        void Add(ENotificationType notificationType, string message);
        void AddSuccess(string message);
        void AddError(string message);
        void Clear();
    }
}