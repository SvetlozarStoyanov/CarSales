using CarSales.Core.Enums;
using CarSales.Core.Models.Reviews;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> CanCreateReviewAsync(string userId, int reviewId);

        Task<IEnumerable<ReviewListModel>> GetLatestReviewsAsync(int? id);

        Task<ReviewListModel> GetNewestReviewAsync();

        Task<ReviewsQueryModel> GetAllReviewsAsync(
            string? searchTerm = null,
            string? vehicleName = null,
            int reviewsPerPage = 6,
            int currentPage = 1,
            string? selectedReviewTypes = null,
            string? selectedVehicleTypes = null,
            ReviewSorting reviewSorting = ReviewSorting.VehiclePriceDescending);


        Task<ReviewViewModel> GetReviewByIdAsync(int id);

        Task<ReviewsQueryModel> GetReviewerReviewsAsync(string userId,
            string? searchTerm = null,
            string? vehicleName = null,
            int reviewsPerPage = 6,
            int currentPage = 1,
            string? selectedReviewTypes = null,
            string? selectedVehicleTypes = null,
            ReviewStatus? reviewStatus = null,
            ReviewSorting reviewSorting = ReviewSorting.VehiclePriceDescending
            );


        Task<ReviewOrderModel> CreateReviewOrderModel(int reviewerId, int vehicleId, IDictionary<ReviewType, decimal> reviewTypesAndPrices);


        Task<ReviewCreateModel> CreateReviewCreateModelAsync(int id);


        Task CreateOrderedReviewAsync(string userId, ReviewOrderModel model);


        Task CreateCompletedReviewAsync(ReviewCreateModel model);

    }
}
