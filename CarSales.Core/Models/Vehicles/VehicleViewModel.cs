using CarSales.Core.Models.Offers;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int YearProduced { get; set; }
        public double TopSpeed { get; set; }
        public double KilometersDriven { get; set; }
        public decimal Price { get; set; }
        public int ReviewCount { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public int? OwnerId { get; set; }
        public string? OwnerUserId { get; set; }
        public string? OwnerName { get; set; }
        public int? SalesmanId { get; set; }
        public string? SalesmanUserId { get; set; }
        public string? SalesmanName { get; set; }
        public int? ImporterId { get; set; }
        public string? ImporterUserId { get; set; }
        public string? ImporterName { get; set; }
        public OfferCreateModel? OfferCreateModel { get; set; }
    }
}
