
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Reviewers
{
    public class ReviewerPriceEditModel
    {
        public int Id { get; set; }
        [Range(50.0, 1000)]
        public decimal ShortReviewPrice { get; set; }
        [Range(75.0, 1500)]
        public decimal StandartReviewPrice { get; set; }
        [Range(100.0, 2000)]
        public decimal PremiumReviewPrice { get; set; }
    }
}
