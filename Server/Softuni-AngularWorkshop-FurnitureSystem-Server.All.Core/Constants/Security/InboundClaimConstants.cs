using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security
{
    public static class InboundClaimConstants
    {
        public const string JwtId = JwtRegisteredClaimNames.Jti;
        public const string SubjectId = ClaimTypes.NameIdentifier;
        public const string UniqueName = ClaimTypes.Name;
        public const string Email = ClaimTypes.Email;
        public const string UpdatedAt = "updated_at";
        public const string Role = ClaimTypes.Role;
    }
}