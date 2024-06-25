using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoravianStar.WebAPI.Attributes;
using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Security;
using System;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorizeResponseModel>> Login([FromBody] LogInWithPasswordRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var accessToken = await authenticationService.LogInWithPasswordAsync(model.Email, model.Password);

            var response = new AuthorizeResponseModel()
            {
                AccessToken = accessToken.Token
            };
            Response.Headers.Append("Cache-Control", "no-store");
            Response.Headers.Append("Pragma", "no-cache");

            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ExecuteInTransactionAsync]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await authenticationService.RegisterAsync(model);

            return Ok();
        }
    }
}