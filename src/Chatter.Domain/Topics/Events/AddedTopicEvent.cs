using System;
using Chatter.Domain.Topics.Events.Base;

namespace Chatter.Domain.Topics.Events
{
    public class AddedTopicEvent : BaseTopicEvent
    {
        public AddedTopicEvent(int id, int userId, int categoryId, string title, string description, DateTime created, bool active, bool removed)
        {
            Id = id;
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Created = created;
            Active = active;
            Removed = removed;
            AggregateId = id;
        }
    }
}