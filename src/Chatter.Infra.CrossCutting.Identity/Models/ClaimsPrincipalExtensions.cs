using System;
using System.Security.Claims;

namespace Chatter.Infra.CrossCutting.Identity.Models
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserIdentityId(this ClaimsPrincipal principal)
        {
            if(principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
    }
}