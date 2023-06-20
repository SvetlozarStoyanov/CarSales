using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleReviewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
    }
}
