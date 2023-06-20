
using CarSales.Core.Models.Reviewers;
using CarSales.Infrastructure.Data.Entities;

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

        Task<ReviewersQueryModel> GetAllReviewersAsync(string searchTerm = null
            );



        Task<ReviewersQueryModel> GetAllReviewersAsync(string userId,
            int vehicleId,
            string? searchTerm = null
            );


        Task<ReviewerPriceEditModel> CreateReviewerPriceEditModelAsync(string userId);


        Task EditReviewPricesAsync(ReviewerPriceEditModel model);
    }
}
