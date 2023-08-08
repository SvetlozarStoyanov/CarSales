using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewMinModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ReviewerName { get; set; } = null!;
        public VehicleRating VehicleRating { get; set; }
    }
}
