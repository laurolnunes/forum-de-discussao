using System;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Topics.Events
{
    public class RemovedTopicEvent : Event
    {
        public RemovedTopicEvent(int id, int userId, int categoryId, string title, string description, DateTime created, bool active, bool removed)
        {
            Id = id;
            AggregateId = id;
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Created = created;
            Removed = removed;
        }

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