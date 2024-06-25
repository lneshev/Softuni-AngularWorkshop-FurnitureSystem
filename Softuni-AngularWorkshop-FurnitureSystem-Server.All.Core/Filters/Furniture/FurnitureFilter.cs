using MoravianStar.Dao;
using MoravianStar.Extensions;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using System;
using System.Linq;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Filters.Furniture
{
    public class FurnitureFilter : FilterSorterBase<FurnitureEntity>
    {
        public Guid? CreatedById { get; set; }

        public override IQueryable<FurnitureEntity> Filter<TDbContext>(IQueryable<FurnitureEntity> query, IEntityRepository<FurnitureEntity, TDbContext> entityRepository)
        {
            query = base.Filter(query, entityRepository);

            if (!CreatedById.IsNullOrEmpty())
            {
                query = query.Where(x => x.CreatedById == CreatedById);
            }

            return query;
        }
    }
}