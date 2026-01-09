using System;
using Onix.Framework.Notifications.Enums;
using Onix.Framework.Notifications.Interfaces;

namespace Onix.Framework.Notifications.Implementation
{
    public struct Notification(ENotificationType notificationType, string message) : INotification, IEquatable<INotification>
    {
        public ENotificationType NotificationType { get; set; } = notificationType;
        public string Message { get; set; } = message;
        public bool IsError => NotificationType == ENotificationType.Error;

        public bool Equals(INotification other) =>
            other.NotificationType == this.NotificationType
            && other.Message == this.Message;
    }
}