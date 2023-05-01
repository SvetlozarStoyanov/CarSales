using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleSellModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public VehicleRating[] VehicleRatings { get; set; }
        public string? OwnerUserId { get; set; }
    }
}
