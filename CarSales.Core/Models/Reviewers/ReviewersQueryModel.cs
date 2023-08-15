using CarSales.Core.Enums;

namespace CarSales.Core.Models.Reviewers
{
    public class ReviewersQueryModel
    {
        public ReviewersQueryModel()
        {
            Reviewers = new List<ReviewerListModel>();
            SortingOptions = Enum.GetValues<ReviewerSorting>().ToHashSet();
        }

        public int ReviewersPerPage { get; set; } = 6;
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int FirstPage { get; set; } = 1;
        public int MaxPage { get; set; }
        public int ReviewsCount { get; set; }
        public int? VehicleId { get; set; }
        public ReviewerSorting ReviewerSorting { get; set; }
        public ICollection<ReviewerSorting> SortingOptions { get; set; }
        public IEnumerable<int> PreviousPages { get; set; }
        public IEnumerable<int> NextPages { get; set; }
        public ICollection<ReviewerListModel> Reviewers { get; set; }
    }
}
