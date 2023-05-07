using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleImportModel
    {
        [Required]
        public string Brand { get; set; } = null!;
        [Required]
        public string ModelName { get; set; } = null!;
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int YearProduced { get; set; }
        public double TopSpeed { get; set; }
        public double KilometersDriven { get; set; } = 0;
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public decimal Price { get; set; }
        public int? ImporterId { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }
        public List<VehicleRating> VehicleRatings { get; set; }
    }
}
