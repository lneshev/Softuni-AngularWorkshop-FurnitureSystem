using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Security
{
    public class SigningConfigurationService
    {
        public SecurityKey Key { get; }

        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurationService()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}