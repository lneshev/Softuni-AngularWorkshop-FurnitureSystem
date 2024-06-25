using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Constants;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.EntityConfigurations.Security
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable(DbSchemaConstants.UserRole, DbSchemaConstants.DboSchema);

            builder.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}