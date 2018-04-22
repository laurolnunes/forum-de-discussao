using System;
using Chatter.Domain.Topics.Events.Base;

namespace Chatter.Domain.Topics.Events
{
    public class UpdatedTopicEvent : BaseTopicEvent
    {
        public UpdatedTopicEvent(int id, int userId, int categoryId, string title, string description, DateTime created)
        {
            Id = id;
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
            AggregateId = id;
            Created = created;
        }
    }
}