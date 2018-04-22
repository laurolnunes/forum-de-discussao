using System;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Users.Events
{
    public class AddedUserEvent : Event
    {
        public AddedUserEvent(int id, string name, string email, Guid identityId)
        {
            Id = id;
            Name = name;
            Email = email;
            IdentityId = identityId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Guid IdentityId { get; set; }
    }
}