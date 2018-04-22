using Chatter.Domain.Core.Commands;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T myCommand) where T : Command;
        void RaiseEvent<T>(T myEvent) where T : Event;
    }
}