using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleEditModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; } 
        public string? DefaultImageUrl { get; set; } 
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
    }
}
