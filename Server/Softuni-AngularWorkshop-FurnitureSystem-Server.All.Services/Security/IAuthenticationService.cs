using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.DTOs.Security;
using Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Security;
using System.Threading.Tasks;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Services.Security
{
    public interface IAuthenticationService
    {
        Task<AccessTokenModel> LogInWithPasswordAsync(string email, string password);
        Task RegisterAsync(RegisterModel model);
    }
}