using Chatter.Domain.Core.Bus;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using FluentValidation.Results;

namespace Chatter.Domain.CommandsHandler
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notification;

        protected CommandHandler(IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notification)
        {
            _uow = uow;
            _bus = bus;
            _notification = notification;
        }

        protected void NotifyErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                var errorMessage = error.ErrorMessage;
                _bus.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected bool Commit()
        {
            if (_notification.HasNotifications())
                return false;

            var commandResponse = _uow.Commit();
            if (commandResponse.Success)
                return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "Error on saving the data."));
            return false;
        }
    }
}