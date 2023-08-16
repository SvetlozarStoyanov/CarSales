using CarSales.Core.Models.Reviews;

namespace CarSales.Core.Models.Reviewers
{
    public class ReviewerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool  CanCreateReview { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public int? VehicleId { get; set; }
        public decimal? ShortReviewPrice { get; set; }
        public decimal? StandartReviewPrice { get; set; }
        public decimal? PremiumReviewPrice { get; set; }
        public IEnumerable<ReviewListModel> Reviews { get; set; }
    }
}
