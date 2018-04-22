using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Chatter.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }

        Guid GetUserIdentityId();

        int GetUserId();

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}