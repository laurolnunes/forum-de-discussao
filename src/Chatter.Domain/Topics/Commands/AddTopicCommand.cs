using Chatter.Domain.Topics.Commands.Base;

namespace Chatter.Domain.Topics.Commands
{
    public class AddTopicCommand : BaseTopicCommand
    {
        public AddTopicCommand(int userId, int categoryId, string title, string description)
        {
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
        }
    }
}