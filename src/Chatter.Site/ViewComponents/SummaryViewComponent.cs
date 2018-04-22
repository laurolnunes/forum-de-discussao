using System.Threading.Tasks;
using Chatter.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Site.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public SummaryViewComponent(IDomainNotificationHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notifications.Notifications());
            notifications.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Value));

            return View();
        }
    }
}
