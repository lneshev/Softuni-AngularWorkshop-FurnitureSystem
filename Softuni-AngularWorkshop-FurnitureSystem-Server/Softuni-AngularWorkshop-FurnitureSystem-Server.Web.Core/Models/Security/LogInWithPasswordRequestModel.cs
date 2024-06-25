using System.ComponentModel.DataAnnotations;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Security
{
    public class LogInWithPasswordRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}