using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Configuration;
using System.Text;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Security
{
    public class SigningConfigurationService
    {
        public SecurityKey Key { get; }

        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurationService(IConfiguration configuration)
        {
            var authenticationConfigSection = configuration.GetSection(nameof(AuthenticationConfiguration));
            var authenticationConfig = authenticationConfigSection.Get<AuthenticationConfiguration>();
            var secretKey = Encoding.UTF8.GetBytes(authenticationConfig.SecretKey);

            Key = new SymmetricSecurityKey(secretKey);
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}