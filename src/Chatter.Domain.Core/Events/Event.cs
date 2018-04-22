using System;

namespace Chatter.Domain.Core.Events
{
    public abstract class Event : Message
    {
        protected Event()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; private set; }
    }
}