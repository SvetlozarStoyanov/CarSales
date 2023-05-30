using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Offers
{
    public class OfferListModel
    {
        public int Id { get; set; }
        public OfferStatus Status { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public int? OfferorId { get; set; }
        public string? OfferorName { get; set; }
        public int? SalesmanId { get; set; }
        public string? SalesmanName { get; set; }
        public decimal Price { get; set; }
    }
}
