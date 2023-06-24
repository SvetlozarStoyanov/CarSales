using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string Title { get; set; }
        public string Overview { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string Performance { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string? Interior { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string? Longevity { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string? Features { get; set; }
        public ReviewType ReviewType { get; set; }
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.Ordered;
        public VehicleRating VehicleRating { get; set; }
        public decimal Price { get; set; }
        public int ReviewerId { get; set; }
        public Reviewer Reviewer { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
