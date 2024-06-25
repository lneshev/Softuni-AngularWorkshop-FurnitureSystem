using Microsoft.AspNetCore.Http;
using MoravianStar.Dao;
using MoravianStar.Exceptions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Furniture
{
    public class FurnitureSaving : IEntitySaving<FurnitureEntity>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public FurnitureSaving(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task SavingAsync(FurnitureEntity entity, FurnitureEntity originalEntity, IDictionary<string, object> additionalParameters = null)
        {
            var isLoggedInUserInAdminRole = httpContextAccessor.HttpContext.User.IsInRole(RoleConstants.SuperAdminRoleName);

            if (!entity.IsNew() && !isLoggedInUserInAdminRole)
            {
                throw new BusinessException(Strings.YouAreNotAllowedToEditThisFurniture);
            }

            await Task.CompletedTask;
        }
    }
}