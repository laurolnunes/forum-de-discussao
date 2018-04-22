using System.Linq;
using Chatter.Domain.CommandsHandler;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Core.Events;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Users.Commands;
using Chatter.Domain.Users.Events;
using Chatter.Domain.Users.Repository;

namespace Chatter.Domain.Users.CommandsHandler
{
    public class UserCommandHandler : CommandHandler,
        IHandler<AddUserCommand>
    {
        private readonly IBus _bus;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUnitOfWork uow, 
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notification, IUserRepository userRepository) : base(uow, bus, notification)
        {
            _bus = bus;
            _userRepository = userRepository;
        }

        public void Handle(AddUserCommand message)
        {
            var user = new User(message.Name, message.Email, message.IdentityId);
            if (!user.IsValid())
            {
                NotifyErrors(user.ValidationResult);
                return;
            }

            var existentUser = _userRepository.Find(u => u.Email == user.Email);
            if (existentUser.Any())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "E-mail já existente"));
            }

            _userRepository.Add(user);
            if (Commit())
            {
                _bus.RaiseEvent(new AddedUserEvent(user.Id, user.Name, user.Email, user.IdentityId));
            }
        }
    }
}