using System.Collections.Generic;
using System.Linq;
using WebRepository.Models;
using WebRepository.Notifications.Contracts;

namespace WebRepository.Notifications.Impl
{
    public class APINotificationManager : IAPINotification
    {
        private IList<APINotificationModel> Notifications;

        public APINotificationManager()
        {
            Notifications = new List<APINotificationModel>();
        }

        public IList<APINotificationModel> GetNotifications()
        {
            return Notifications;
        }

        public void Handle(APINotificationModel notification)
        {
            Notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return Notifications.Any();
        }
    }
}
