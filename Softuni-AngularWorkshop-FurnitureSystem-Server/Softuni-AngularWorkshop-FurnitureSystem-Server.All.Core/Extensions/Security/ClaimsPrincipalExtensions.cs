using MoravianStar.Extensions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Resources;
using System;
using System.Linq;
using System.Security;
using System.Security.Claims;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Extensions.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid DeserializeIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            var userIdString = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == InboundClaimConstants.SubjectId)?.Value;
            Guid.TryParse(userIdString, out Guid userId);
            if (userId.IsNullOrEmpty())
            {
                throw new SecurityException(Strings.UserIDIsNotProvidedInTheClaims);
            }
            return userId;
        }

        public static string DeserializeEmailClaim(this ClaimsPrincipal claimsPrincipal)
        {
            var userEmail = claimsPrincipal.Claims.Single(x => x.Type == InboundClaimConstants.Email).Value;
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                throw new SecurityException(Strings.UserEmailIsNotProvidedInTheClaims);
            }
            return userEmail;
        }
    }
}