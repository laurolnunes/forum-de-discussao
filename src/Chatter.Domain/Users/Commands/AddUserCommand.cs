using System;
using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Users.Commands
{
    public class AddUserCommand : Command
    {
        public AddUserCommand(string name, string email, Guid identityId)
        {
            Name = name;
            Email = email;
            IdentityId = identityId;
        }
        public string Name { get; set; }

        public string Email { get; set; }

        public Guid IdentityId { get; set; }
    }
}