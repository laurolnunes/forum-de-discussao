using Chatter.Domain.Core.Events;
using Chatter.Domain.Users.Events;

namespace Chatter.Domain.Users.EventsHandler
{
    public class UserEventHandler : IHandler<AddedUserEvent>
    {
        public void Handle(AddedUserEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }
    }
}