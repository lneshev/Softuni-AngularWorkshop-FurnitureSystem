using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Infrastructure.Handlers
{
    public class JwtEventHandlers
    {
        public async Task OnTokenValidated(TokenValidatedContext context)
        {
            //var userModifiedAtCacheService = ServiceLocator.Container.GetRequiredService<IUserModifiedAtCacheService>();

            //var userId = context.Principal.DeserializeIdClaim();

            //var userModifiedAtClaim = context.Principal.DeserializeModifiedAtClaim();
            //var userModifiedAtCache = await GetUserModifiedAtFromCache(userModifiedAtCacheService, userId);

            //if (userModifiedAtClaim < userModifiedAtCache)
            //{
            //    throw new SecurityTokenExpiredException(string.Format(Strings.UserModifiedAtTimestampIsOlderThanCurrentTimestampInTheUserClaims, userId));
            //}

            await Task.CompletedTask;
        }

        public async Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Append("Token-Expired", "true");
            }
            await Task.CompletedTask;
        }

        //private async Task<DateTimeOffset> GetUserModifiedAtFromCache(IUserModifiedAtCacheService userModifiedAtCacheService, Guid userId)
        //{
        //    var userModifiedAtCache = await userModifiedAtCacheService.GetAsync(userId);
        //    if (!userModifiedAtCache.HasValue)
        //    {
        //        var userEntityRepositoryService = ServiceLocator.Container.GetRequiredService<IUserEntityRepositoryService>();
        //        var user = await userEntityRepositoryService.GetAsync(userId);
        //        userModifiedAtCache = await userModifiedAtCacheService.SaveAsync(user);
        //    }

        //    return userModifiedAtCache.Value;
        //}
    }
}