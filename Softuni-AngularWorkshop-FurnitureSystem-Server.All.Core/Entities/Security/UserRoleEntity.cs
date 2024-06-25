using Microsoft.AspNetCore.Identity;
using MoravianStar.Dao;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security
{
    public class UserRoleEntity : IdentityUserRole<Guid>, IEntityBase<Guid>
    {
        [NotMapped]
        public Guid Id
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public bool IsNew()
        {
            throw new NotSupportedException();
        }

        public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}