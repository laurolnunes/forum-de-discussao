using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Topics.Commands
{
    public class AddPostCommand : Command
    {
        public AddPostCommand(int userId, int topicId, string text)
        {
            UserId = userId;
            TopicId = topicId;
            Text = text;    
        }

        public int UserId { get; set; }
        public int TopicId { get; set; }
        public string Text { get; set; }
    }
}