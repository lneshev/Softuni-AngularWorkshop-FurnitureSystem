namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Configuration
{
    public class AuthenticationConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public long AccessTokenExpiration { get; set; }
        public long RefreshTokenExpiration { get; set; }
    }
}