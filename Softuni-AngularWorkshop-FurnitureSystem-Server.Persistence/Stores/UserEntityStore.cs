using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.DbContexts;
using System;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Stores
{
    public class UserEntityStore : UserStore<UserEntity, RoleEntity, AppDbContext, Guid>
    {
        public UserEntityStore(AppDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}