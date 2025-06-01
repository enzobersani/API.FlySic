using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Notifications
{
    public interface INotificationService
    {
        void AddNotification(string key, string message);
        bool HasNotifications();
        List<Notification> GetNotifications();
    }

    public class NotificationService : INotificationService
    {
        private readonly List<Notification> _notifications;

        public NotificationService()
        {
            _notifications = new List<Notification>();
        }
        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }
    }
}
