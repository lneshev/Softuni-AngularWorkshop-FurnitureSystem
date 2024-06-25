using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security
{
    public static class OutboundClaimConstants
    {
        public const string JwtId = JwtRegisteredClaimNames.Jti;
        public const string SubjectId = JwtRegisteredClaimNames.Sub;
        public const string UniqueName = JwtRegisteredClaimNames.UniqueName;
        public const string Email = JwtRegisteredClaimNames.Email;
        public const string UpdatedAt = "updated_at";
        public const string Role = ClaimTypes.Role;
    }
}