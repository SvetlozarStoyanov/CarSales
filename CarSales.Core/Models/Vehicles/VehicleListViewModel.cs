using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public decimal Price { get; set; }
        public int? SalesmanId { get; set; }
        public string? SalesmanName { get; set; }

    }
}
