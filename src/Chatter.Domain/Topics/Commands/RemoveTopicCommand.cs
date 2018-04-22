using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Topics.Commands
{
    public class RemoveTopicCommand : Command
    {
        public RemoveTopicCommand(int id)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; set; }
    }
}