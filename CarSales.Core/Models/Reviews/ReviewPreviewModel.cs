using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewPreviewModel
    {
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? Performance { get; set; } 
        public string? Interior { get; set; } 
        public string? Longevity { get; set; }
        public string? Features { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public int VehicleRatingAsInt { get; set; }
        public VehicleReviewModel Vehicle { get; set; }

    }
}
