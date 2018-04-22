using Chatter.Domain.Topics.Events.Base;

namespace Chatter.Domain.Topics.Events
{
    public class RemovedPostEvent : BaseTopicEvent
    {
        public RemovedPostEvent(int id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}