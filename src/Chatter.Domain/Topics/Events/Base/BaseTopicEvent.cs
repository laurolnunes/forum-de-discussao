using System;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Topics.Events.Base
{
    public abstract class BaseTopicEvent : Event
    {
        public int Id { get; protected set; }
        public int UserId { get; protected set; }
        public int CategoryId { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public DateTime Created { get; protected set; }
        public bool Active { get; protected set; }
        public bool Removed { get; protected set; }
    }
}