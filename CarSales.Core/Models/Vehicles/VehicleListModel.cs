using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleListModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public decimal Price { get; set; }
        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerUserId { get; set; }
        public int? SalesmanId { get; set; }
        public string? SalesmanName { get; set; }
        public string? SalesmanUserId { get; set; }
        public int? ImporterId { get; set; }
        public string? ImporterName { get; set; }
        public string? ImporterUserId { get; set; }
    }
}
