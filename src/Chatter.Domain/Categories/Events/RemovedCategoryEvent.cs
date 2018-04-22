using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Categories.Events
{
    public class RemovedCategoryEvent : Event
    {
        public RemovedCategoryEvent(int id)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; set; }
    }
}