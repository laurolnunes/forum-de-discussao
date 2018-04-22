using System;
using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Topics.Commands.Base
{
    public abstract class BaseTopicCommand : Command
    {
        public int Id { get; protected set; }
        public int UserId { get; protected set; }
        public int CategoryId { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public DateTime Created { get; protected set; }
        public bool Active { get; protected set; }
        public bool Removed { get; protected set; }
    }
}