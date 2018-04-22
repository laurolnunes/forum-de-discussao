using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Core.Notifications
{
    public class DomainNotification : Event 
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            Version = 1;
        }
        public int Id { get; protected set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }
    }
}