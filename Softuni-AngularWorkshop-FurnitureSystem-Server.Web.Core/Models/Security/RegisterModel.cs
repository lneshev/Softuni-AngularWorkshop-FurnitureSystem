using Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Constants.Security;
using System.ComponentModel.DataAnnotations;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Web.Core.Models.Security
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(UserEntityConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}