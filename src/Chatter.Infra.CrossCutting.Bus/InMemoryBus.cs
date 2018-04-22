using System;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Core.Commands;
using Chatter.Domain.Core.Events;
using Chatter.Domain.Core.Notifications;

namespace Chatter.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IBus
    {
        public static Func<IServiceProvider> ContainerAccessor { get; set; }

        private static IServiceProvider Container => ContainerAccessor();

        public void SendCommand<T>(T myCommand) where T : Command
        {
            Publish(myCommand);
        }

        public void RaiseEvent<T>(T myEvent) where T : Event
        {
            Publish(myEvent);
        }

        private static void Publish<T>(T message) where T : Message
        {
            if (Container == null) return;
            var obj = Container.GetService(message.MessageType.Equals(nameof(DomainNotification))
                ? typeof(IDomainNotificationHandler<T>)
                : typeof(IHandler<T>));

            ((IHandler<T>)obj).Handle(message);
        }
    }
}