using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Categories.Commands
{
    public class RemoveCategoryCommand : Command
    {
        public RemoveCategoryCommand(int id)
        {
            Id = id;
            AggregateId = id;
        }

        public int Id { get; set; }
    }
}