using Onix.Framework.Notifications.Enums;
using Onix.Framework.Notifications.Implementation;
using Xunit;

namespace Onix.Framework.Tests
{
    public class NotificationsTests
    {
        [Fact]
        public void Deve_verificar_adicao_notificacao_duplicada()
        {
            var notificacao1 = GetNotification();
            var notificacao2 = GetNotification();
            var notificationContext = new NotificationContext();
            notificationContext.Add(notificacao1);
            Assert.Single(notificationContext.GetNotifications());
            notificationContext.Add(notificacao2);
            Assert.Single(notificationContext.GetNotifications());
        }

        private Notification GetNotification()
        {
            return new Notification(ENotificationType.Error, "TEST");  
        }
    }
}