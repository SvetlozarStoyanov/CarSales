using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviewers
{
    public class ReviewersQueryModel
    {
        public ReviewersQueryModel()
        {
            Reviewers = new List<ReviewerListModel>();
        }

        public string? SearchTerm { get; set; }
        public int? VehicleId { get; set; }
        public ICollection<ReviewerListModel> Reviewers { get; set; }
    }
}
