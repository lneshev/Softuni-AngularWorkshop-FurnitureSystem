using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MoravianStar.Exceptions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.DTOs.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Resources;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly SignInManager<UserEntity> signInManager;
        private readonly SigningConfigurationService signingConfigurationService;

        public AuthenticationService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            SigningConfigurationService signingConfigurationService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.signingConfigurationService = signingConfigurationService;
        }

        public async Task<AccessTokenModel> LogInWithPasswordAsync(string email, string password)
        {
            var user = await FindUserByEmail(email);

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!signInResult.Succeeded)
            {
                throw new BusinessException(Strings.LogInWasUnsuccessful);
            }

            var accessToken = await CreateAccessTokenAsync(user);

            return accessToken;
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var id = Guid.NewGuid();
                user = new UserEntity
                {
                    Id = id,
                    UserName = model.Name,
                    Email = model.Email,
                    EmailConfirmed = true,
                    CreatedById = id,
                    CreatedAt = DateTimeOffset.Now
                };

                var identityResult = await userManager.CreateAsync(user, model.Password);
                if (!identityResult.Succeeded)
                {
                    throw new BusinessException(string.Format(Strings.RegistrationFailedReason, identityResult.Errors.First().Description));
                }

                await userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                throw new BusinessException(string.Format(Strings.RegistrationFailedUserExists, model.Email));
            }
        }

        private async Task<UserEntity> FindUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new EntityNotFoundException(string.Format(Strings.UserWithEmailWasNotFound, email));
            }
            return user;
        }

        private async Task<AccessTokenModel> CreateAccessTokenAsync(UserEntity user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var accessToken = await BuildAccessTokenAsync(user);

            return accessToken;
        }

        private async Task<AccessTokenModel> BuildAccessTokenAsync(UserEntity user)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddYears(1);
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(await GetUserClaimsAsync(user)),
                Claims = null,
                Issuer = "http://localhost:5000",
                Audience = "SoftuniFurnitureWorkshopAudience",
                Expires = accessTokenExpiration,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = signingConfigurationService.SigningCredentials
            };
            var securityToken = handler.CreateToken(tokenDescriptor);
            var accessToken = handler.WriteToken(securityToken);

            return new AccessTokenModel { Token = accessToken };
        }

        private async Task<IEnumerable<Claim>> GetUserClaimsAsync(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(OutboundClaimConstants.JwtId, Guid.NewGuid().ToString()),
                new Claim(OutboundClaimConstants.SubjectId, user.Id.ToString()),
                new Claim(OutboundClaimConstants.UniqueName, user.UserName),
                new Claim(OutboundClaimConstants.Email, user.Email)
            };

            var roleNames = await userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                claims.Add(new Claim(OutboundClaimConstants.Role, roleName));
            }

            return claims;
        }
    }
}
