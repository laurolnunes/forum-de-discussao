using System;
using Chatter.Domain.Topics.Commands.Base;

namespace Chatter.Domain.Topics.Commands
{
    public class UpdateTopicCommand : BaseTopicCommand
    {
        public UpdateTopicCommand(int id, int userId, int categoryId, string title, string description, DateTime created)
        {
            Id = Id;
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Created = created;
        }
    }
}