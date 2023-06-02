namespace CarSales.Core.Models.Offers
{
    public class OfferEditModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal AvailableCredits { get; set; }
    }
}
