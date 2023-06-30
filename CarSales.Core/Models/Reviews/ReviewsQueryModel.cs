using CarSales.Core.Enums;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewsQueryModel
    {
        public ReviewsQueryModel()
        {
            ReviewStatuses = Enum.GetValues<ReviewStatus>().ToHashSet();
            ReviewTypes = Enum.GetValues<ReviewType>().ToHashSet();
            VehicleTypes = Enum.GetValues<VehicleType>().ToHashSet();
            SortingOptions = Enum.GetValues<ReviewSorting>().ToHashSet();
            Reviews = new HashSet<ReviewListModel>();
        }

        public string? SearchTerm { get; set; }
        public string? VehicleName { get; set; }
        public int ReviewsPerPage { get; set; } = 6;
        public int CurrentPage { get; set; } = 1;
        public int FirstPage { get; set; } = 1;
        public int MaxPage { get; set; }
        public int ReviewCount { get; set; }
        public string? SelectedVehicleTypes { get; set; }
        public string? SelectedReviewTypes { get; set; }
        public ReviewSorting ReviewSorting { get; set; }
        public ReviewStatus? ReviewStatus { get; set; }
        public ICollection<ReviewSorting> SortingOptions { get; set; }
        public ICollection<ReviewStatus> ReviewStatuses { get; set; }
        public ICollection<ReviewType> ReviewTypes { get; set; }
        public ICollection<string> VehicleNames { get; set; }
        public IEnumerable<int> PreviousPages { get; set; }
        public IEnumerable<int> NextPages { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
        public IEnumerable<ReviewListModel> Reviews { get; set; }
    }
}
