using System.Collections.Generic;
using System.Linq;

namespace Chatter.Domain.Core.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Handle(DomainNotification message)
        {
            _notifications.Add(message);
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public List<DomainNotification> Notifications()
        {
            return _notifications;
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}