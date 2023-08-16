namespace CarSales.Core.Models.Reviewers
{
    public class ReviewerListModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public decimal? ShortReviewPrice { get; set; }
        public decimal? StandartReviewPrice { get; set; }
        public decimal? PremiumReviewPrice { get; set; }
    }
}
