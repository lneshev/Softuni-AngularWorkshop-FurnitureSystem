using Microsoft.AspNetCore.Identity;
using MoravianStar.Dao;
using MoravianStar.Extensions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security
{
    public class UserEntity : IdentityUser<Guid>, IEntityBase<Guid>
    {
        public UserEntity()
        {
            Roles = new List<UserRoleEntity>();
        }

        [Required]
        [StringLength(UserEntityConstants.EmailMaxLength)]
        public override string Email { get; set; }

        public virtual UserEntity CreatedBy { get; set; }
        public Guid CreatedById { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual UserEntity ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public virtual ICollection<UserRoleEntity> Roles { get; set; }

        public bool IsNew()
        {
            return Id.IsNullOrEmpty();
        }
    }
}