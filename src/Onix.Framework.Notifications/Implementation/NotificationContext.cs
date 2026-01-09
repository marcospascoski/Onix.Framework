using System.Collections.Generic;
using System.Linq;
using Onix.Framework.Notifications.Enums;
using Onix.Framework.Notifications.Interfaces;

namespace Onix.Framework.Notifications.Implementation
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<INotification> _notifications = [];


        public bool HasErrors => _notifications.Any(x => x.IsError);

        public NotificationContext()
        {
            Clear();
        }

        public void Add(INotification notification)
        {
            if (!_notifications.Any(x => x.Equals(notification)))
            {
                _notifications.Add(notification);
            }
        }

        public void Add(ENotificationType notificationType, string message)
        {
            Add(new Notification(notificationType, message));
        }

        public void AddError(string message)
        {
            Add(new Notification(ENotificationType.Error, message));
        }

        public void AddSuccess(string message)
        {
            Add(new Notification(ENotificationType.Success, message));
        }

        public void Clear()
        {
            _notifications.Clear();
        }

        public IEnumerable<INotification> GetErrors()
        {
            return _notifications.Where(x => x.IsError);
        }

        public IEnumerable<INotification> GetNotifications()
        {
            return _notifications;
        }
    }
}