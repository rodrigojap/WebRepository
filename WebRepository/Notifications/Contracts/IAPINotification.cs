using System.Collections.Generic;
using WebRepository.Models;

namespace WebRepository.Notifications.Contracts
{
    public interface IAPINotification
    {
        bool HasNotification();
        IList<APINotificationModel> GetNotifications();
        void Handle(APINotificationModel notificacao);
    }
}
