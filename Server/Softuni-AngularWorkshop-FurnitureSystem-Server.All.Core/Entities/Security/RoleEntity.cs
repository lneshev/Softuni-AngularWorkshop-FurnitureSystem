using Microsoft.AspNetCore.Identity;
using MoravianStar.Dao;
using MoravianStar.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security
{
    public class RoleEntity : IdentityRole<Guid>, IEntityBase<Guid>
    {
        public RoleEntity()
        {
            Users = new List<UserRoleEntity>();    
        }

        [Required]
        public override string Name { get; set; }

        public virtual ICollection<UserRoleEntity> Users { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public virtual UserEntity CreatedBy { get; set; }
        public Guid CreatedById { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [ForeignKey(nameof(ModifiedById))]
        public virtual UserEntity ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public bool IsNew()
        {
            return Id.IsNullOrEmpty();
        }
    }
}