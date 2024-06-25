using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Constants;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.EntityConfigurations.Furniture
{
    public class FurnitureEntityConfiguration : IEntityTypeConfiguration<FurnitureEntity>
    {
        public void Configure(EntityTypeBuilder<FurnitureEntity> builder)
        {
            builder.ToTable(DbSchemaConstants.Furniture, DbSchemaConstants.DboSchema);

            builder.Property(x => x.Price).HasPrecision(18, 8);
        }
    }
}