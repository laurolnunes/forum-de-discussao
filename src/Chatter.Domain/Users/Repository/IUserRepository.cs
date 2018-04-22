using System;
using Chatter.Domain.Interfaces;

namespace Chatter.Domain.Users.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        int GetIdByIdentityId(Guid id);
    }
}