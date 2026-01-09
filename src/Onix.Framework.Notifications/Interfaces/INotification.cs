using Onix.Framework.Notifications.Enums;

namespace Onix.Framework.Notifications.Interfaces
{
    public interface INotification
    {
         ENotificationType NotificationType { get; set; }
         string Message { get; set; }
         bool IsError { get; }
    }
}