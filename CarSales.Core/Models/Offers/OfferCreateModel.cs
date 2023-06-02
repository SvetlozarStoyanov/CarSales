namespace CarSales.Core.Models.Offers
{
    public class OfferCreateModel
    {
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal AvailableCredits { get; set; }
        public int VehicleId { get; set; }
        public int OfferorId { get; set; }
        public int SalesmanId { get; set; }
    }
}
