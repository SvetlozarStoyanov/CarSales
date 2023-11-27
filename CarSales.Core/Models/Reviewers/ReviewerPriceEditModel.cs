
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Reviewers
{
    public class ReviewerPriceEditModel
    {
        public int Id { get; set; }
        [Range(50.0, 1000, ErrorMessage = "Must be between 50 and 1000")]
        public decimal ShortReviewPrice { get; set; }
        [Range(75.0, 1500, ErrorMessage = "Must be between 75 and 1500")]
        public decimal StandartReviewPrice { get; set; }
        [Range(100.0, 2000, ErrorMessage = "Must be between 100 and 2000")]
        public decimal PremiumReviewPrice { get; set; }
    }
}
