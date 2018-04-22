using System;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Core.Commands
{
    public class Command : Message
    {
        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
        public DateTime TimeStamp { get; }

    }
}