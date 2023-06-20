using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; } = null!;
        public string Performance { get; set; } = null!;
        public string? Interior { get; set; } = null!;
        public string? Longevity { get; set; }
        public string? Features { get; set; }
        public ReviewType ReviewType { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public VehicleReviewModel Vehicle { get; set; }

    }
}
