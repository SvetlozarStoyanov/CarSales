using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; } = null!;
        public ReviewType ReviewType { get; set; }
        public decimal VehiclePrice { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
    }
}
