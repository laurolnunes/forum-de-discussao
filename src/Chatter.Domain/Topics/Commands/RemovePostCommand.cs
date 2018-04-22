using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Topics.Commands
{
    public class RemovePostCommand : Command
    {
        public RemovePostCommand(int id)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; set; }
    }
}