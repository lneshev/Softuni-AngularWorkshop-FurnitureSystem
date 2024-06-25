using Microsoft.AspNetCore.Http;
using MoravianStar.Dao;
using MoravianStar.Exceptions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Extensions.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Furniture
{
    public class FurnitureDeleting : IEntityDeleting<FurnitureEntity>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public FurnitureDeleting(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task DeletingAsync(FurnitureEntity entity, IDictionary<string, object> additionalParameters = null)
        {
            var loggedInUserId = httpContextAccessor.HttpContext.User.DeserializeIdClaim();
            var isLoggedInUserInAdminRole = httpContextAccessor.HttpContext.User.IsInRole(RoleConstants.SuperAdminRoleName);

            if (!isLoggedInUserInAdminRole && entity.CreatedById != loggedInUserId)
            {
                throw new BusinessException(Strings.YouAreNotAllowedToDeleteThisFurniture);
            }

            await Task.CompletedTask;
        }
    }
}