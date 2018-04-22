using System;
using System.Collections.Generic;
using System.Security.Claims;
using Chatter.Application.Interfaces;
using Chatter.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Chatter.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserApplicationService _userApplicationService;

        public AspNetUser(IHttpContextAccessor accessor, IUserApplicationService userApplicationService)
        {
            _accessor = accessor;
            _userApplicationService = userApplicationService;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserIdentityId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserIdentityId()) : Guid.NewGuid();
        }

        public int GetUserId()
        {
            return IsAuthenticated() ? _userApplicationService.GetIdByIdentityId(GetUserIdentityId()) : 0 ;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}