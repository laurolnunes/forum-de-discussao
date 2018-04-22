namespace Chatter.Domain.Core.Events
{
    public abstract class Message
    {
        protected Message()
        {
            MessageType = GetType().Name;
        }
        public string MessageType { get; }
        public int AggregateId { get; protected set; }
    }
}