using System;
using System.ComponentModel.DataAnnotations;

namespace Softuni_AngularWorkshop_FurnitureSystem_Server.All.Core.Entities.Furniture
{
    public class FurnitureEntity : TrackableEntityBase<int>
    {
        [Required]
        [MinLength(4)]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(1950, 2050)]
        public int Year { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 1000000000000)]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }

        public string Material { get; set; }
    }
}