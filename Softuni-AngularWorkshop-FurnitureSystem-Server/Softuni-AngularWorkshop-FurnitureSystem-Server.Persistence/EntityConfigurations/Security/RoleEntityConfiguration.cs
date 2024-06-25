using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Constants;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.EntityConfigurations.Security
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable(DbSchemaConstants.Role, DbSchemaConstants.DboSchema);

            builder.HasMany(x => x.Users).WithOne(y => y.Role).HasForeignKey(y => y.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}