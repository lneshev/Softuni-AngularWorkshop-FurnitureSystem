using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Constants;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.EntityConfigurations.Security
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(DbSchemaConstants.User, DbSchemaConstants.DboSchema);

            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(y => y.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(y => y.ModifiedById);

            builder.HasMany(x => x.Roles).WithOne(y => y.User).HasForeignKey(y => y.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}