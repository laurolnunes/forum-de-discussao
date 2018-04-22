using System;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Site.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IUser _user;

        public Guid UserIdentityId { get; set; }
        public int UserId { get; set; }

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications, IUser user)
        {
            _notifications = notifications;
            _user = user;
            if (_user.IsAuthenticated())
            {
                UserIdentityId = _user.GetUserIdentityId();
                UserId = _user.GetUserId();
            }
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }
    }
}