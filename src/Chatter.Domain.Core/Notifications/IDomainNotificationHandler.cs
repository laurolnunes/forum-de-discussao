using System.Collections.Generic;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();

        List<T> Notifications();
    }
}