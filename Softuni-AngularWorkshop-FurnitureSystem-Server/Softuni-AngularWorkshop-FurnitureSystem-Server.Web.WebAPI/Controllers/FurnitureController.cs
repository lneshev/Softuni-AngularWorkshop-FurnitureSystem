using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoravianStar.Dao;
using MoravianStar.WebAPI.Controllers;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Extensions.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Filters.Furniture;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.DbContexts;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Furniture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FurnitureController : EntityRestController<FurnitureEntity, int, FurnitureModel, FurnitureFilter, AppDbContext>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public FurnitureController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("all")]
        public Task<ActionResult<PageResult<FurnitureModel>>> ReadAll([FromQuery] List<Sort> sorts, [FromQuery] Page page)
        {
            return base.Read(new FurnitureFilter(), sorts, page);
        }

        [HttpGet("user")]
        public Task<ActionResult<PageResult<FurnitureModel>>> ReadForLoggedInUser([FromQuery] List<Sort> sorts, [FromQuery] Page page)
        {
            return base.Read(new FurnitureFilter() { CreatedById = httpContextAccessor?.HttpContext?.User?.DeserializeIdClaim() }, sorts, page);
        }

        [HttpGet("details/{id}")]
        public override Task<ActionResult<FurnitureModel>> Get([FromRoute] int id)
        {
            return base.Get(id);
        }

        [HttpPost("create")]
        public override Task<ActionResult<FurnitureModel>> Post([FromBody] FurnitureModel model)
        {
            return base.Post(model);
        }

        [HttpPost("edit/{id}")]
        public override Task<ActionResult<FurnitureModel>> Put([FromRoute] int id, [FromBody] FurnitureModel model)
        {
            return base.Put(id, model);
        }

        [HttpDelete("delete/{id}")]
        public override async Task<ActionResult<FurnitureModel>> Delete([FromRoute] int id)
        {
            return await base.Delete(id);
        }
    }
}