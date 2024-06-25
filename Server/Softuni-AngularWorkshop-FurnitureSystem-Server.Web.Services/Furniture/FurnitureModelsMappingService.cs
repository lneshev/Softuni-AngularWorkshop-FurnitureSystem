using MoravianStar.Dao;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Furniture;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Services.Furniture
{
    public class FurnitureModelsMappingService : ModelsMappingService<FurnitureModel, FurnitureEntity>
    {
        public override Expression<Func<FurnitureEntity, IProjectionBase>> Project()
        {
            return x => new FurnitureModel()
            {
                Id = x.Id,
                Make = x.Make,
                Model = x.Model,
                Year = x.Year,
                Description = x.Description,
                Price = x.Price,
                Image = x.Image,
                Material = x.Material
            };
        }

        public override async Task<List<EntityModelPair<FurnitureEntity, FurnitureModel>>> ToEntities(List<EntityModelPair<FurnitureEntity, FurnitureModel>> pairs)
        {
            pairs = await base.ToEntities(pairs);

            foreach (var pair in pairs)
            {
                pair.Entity.Make = pair.Model.Make;
                pair.Entity.Model = pair.Model.Model;
                pair.Entity.Year = pair.Model.Year;
                pair.Entity.Description = pair.Model.Description;
                pair.Entity.Price = pair.Model.Price;
                pair.Entity.Image = pair.Model.Image;
                pair.Entity.Material = pair.Model.Material;
            }

            return pairs;
        }
    }
}