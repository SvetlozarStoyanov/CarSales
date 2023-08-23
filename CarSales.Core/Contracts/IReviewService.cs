using CarSales.Core.Enums;
using CarSales.Core.Models.Reviews;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IReviewService
    {
        /// <summary>
        /// Returns true if <see cref="Reviewer"/> with <paramref name="userId"/> has an 
        /// <see cref="Review"/> with <see cref="ReviewStatus.Ordered"/>, returns false 
        /// if <see cref="Review"/> does not exist, doesn't belong to <see cref="Reviewer"/> with <paramref name="userId"/>
        /// or is <see cref="ReviewStatus.Completed"/> 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reviewId"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> CanCreateReviewAsync(string userId, int reviewId);

        /// <summary>
        /// Returns latest <see cref="Review"/>s 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="ReviewListModel"/></returns>
        Task<IEnumerable<ReviewListModel>> GetLatestReviewsAsync();

        /// <summary>
        /// Returns a random <see cref="Review"/> from the given <paramref name="reviews"/>
        /// </summary>
        /// <returns><see cref="ReviewListModel"/> review</returns>
        Task<ReviewListModel> GetRandomReviewAsync(IEnumerable<ReviewListModel> reviews);

        /// <summary>
        /// Returns all <see cref="Review"/> which match criteria given in parameters
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="vehicleName"></param>
        /// <param name="reviewsPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="selectedReviewTypes"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <param name="reviewSorting"></param>
        /// <returns><see cref="ReviewsQueryModel"/></returns>
        Task<ReviewsQueryModel> GetAllReviewsAsync(
            string? searchTerm = null,
            string? vehicleName = null,
            int reviewsPerPage = 6,
            int currentPage = 1,
            string? selectedReviewTypes = null,
            string? selectedVehicleTypes = null,
            ReviewSorting reviewSorting = ReviewSorting.VehiclePriceDescending);


        /// <summary>
        /// Returns <see cref="Review"/> wih given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ReviewViewModel"/></returns>
        Task<ReviewViewModel> GetReviewByIdAsync(int id);


        /// <summary>
        /// Returns all <see cref="Review"/> which match criteria given in parameters, 
        /// belonging to <see cref="Reviewer"/> with <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchTerm"></param>
        /// <param name="vehicleName"></param>
        /// <param name="reviewsPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="selectedReviewTypes"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <param name="reviewStatus"></param>
        /// <param name="reviewSorting"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a <see cref="ReviewOrderModel"/> which lets <see cref="Salesman"/> choose the <see cref="ReviewType"/>
        /// he wants to order from <see cref="Reviewer"/> with <paramref name="reviewerId"/>
        /// </summary>
        /// <param name="reviewerId"></param>
        /// <param name="vehicleId"></param>
        /// <param name="reviewTypesAndPrices"></param>
        /// <returns><see cref="ReviewOrderModel"/></returns>
        Task<ReviewOrderModel> CreateReviewOrderModel(int reviewerId, int vehicleId, IDictionary<ReviewType, decimal> reviewTypesAndPrices);

        /// <summary>
        /// Creates a <see cref="ReviewCreateModel"/> which lets <see cref="Reviewer"/> create a <see cref="Review"/>
        /// with <see cref="ReviewStatus.Completed"/> from a <see cref="Review"/> with <see cref="ReviewStatus.Ordered"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ReviewCreateModel"/></returns>
        Task<ReviewCreateModel> CreateReviewCreateModelAsync(int id);

        /// <summary>
        /// Creates an <see cref="Review"/> with <see cref="ReviewStatus.Ordered"/> from <paramref name="model"/>
        /// and subtracts the <see cref="Review.Price"/> from <see cref="Salesman"/> with <paramref name="userId"/> who orders it
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateOrderedReviewAsync(string userId, ReviewOrderModel model);

        /// <summary>
        /// Adds the information from <paramref name="model"/> to <see cref="Review"/> with same <see cref="Review.Id"/>
        /// and changes the <see cref="Review.ReviewStatus"/> to <see cref="ReviewStatus.Completed"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateCompletedReviewAsync(ReviewCreateModel model);

        /// <summary>
        /// Returns a <see cref="ReviewPreviewModel"/> using data from <paramref name="model"/>.
        /// Does not require a valid <paramref name="model"/> to work
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="ReviewPreviewModel"/></returns>
        Task<ReviewPreviewModel> CreateReviewPreviewModelAsync(ReviewCreateModel model);

    }
}
