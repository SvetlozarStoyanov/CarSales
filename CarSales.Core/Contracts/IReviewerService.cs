
using CarSales.Core.Enums;
using CarSales.Core.Models.Reviewers;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IReviewerService
    {
        /// <summary>
        /// Creates a new <see cref="Reviewer"/> with given <paramref name="userId"/>,
        /// If <see cref="Reviewer"/> already exists , sets <see cref="Reviewer.IsActive"/> to true and 
        /// sets default <see cref="Reviewer.ShortReviewPrice"/> to 100, <see cref="Reviewer.StandartReviewPrice"/> to 150 and 
        /// <see cref="Reviewer.PremiumReviewPrice"/> to 200 default values.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateOrRenewReviewerAsync(string userId);

        /// <summary>
        /// Sets <see cref="Reviewer.IsActive"/> to false
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RetireReviewerAsync(string userId);


        Task<IDictionary<ReviewType, decimal>> GetReviewTypesAndPricesAsync(int reviewerId);

        /// <summary>
        /// Returns <see cref="ReviewersQueryModel"/> with <see cref="ReviewersQueryModel.Reviewers"/>
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns><see cref="ReviewersQueryModel"/></returns>
        Task<ReviewersQueryModel> GetAllReviewersAsync(string searchTerm = null
            );

        /// <summary>
        /// Returns <see cref="ReviewersQueryModel"/> with <see cref="ReviewersQueryModel.Reviewers"/> who can create
        /// a review for <see cref="Vehicle"/> with <paramref name="vehicleId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vehicleId"></param>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="reviewersPerPage"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<ReviewersQueryModel> GetReviewersAsync(string userId,
            int vehicleId,
            string? searchTerm = null,
            int currentPage = 1,
            int reviewersPerPage = 6,
            ReviewerSorting sorting = ReviewerSorting.Alphabetically
            );

        Task<ReviewerViewModel> GetReviewerByIdAsync(int reviewerId, int vehicleId);

        /// <summary>
        /// Creates <see cref="ReviewerPriceEditModel"/> for editing <see cref="Review.Price"/> of 
        /// individual <see cref="ReviewType"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="ReviewerPriceEditModel"/></returns>
        Task<ReviewerPriceEditModel> CreateReviewerPriceEditModelAsync(string userId);

        /// <summary>
        /// Updates <see cref="Review.Price"/> of all <see cref="Review.ReviewType"/> with
        /// prices from <paramref name="model"/> and saves the changes in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditReviewPricesAsync(ReviewerPriceEditModel model);
    }
}
