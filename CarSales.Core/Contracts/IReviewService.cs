using CarSales.Core.Models.Reviews;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewListModel>> GetLatestReviewsAsync();


        Task<ReviewViewModel> GetReviewByIdAsync(int id);


        Task<ReviewOrderModel> CreateReviewOrderModelAsync(int reviewerId, int vehicleId);


        Task<ReviewCreateModel> CreateReviewCreateModelAsync(int reviewerId, int vehicleId, decimal price, ReviewType reviewType);



        Task CreateOrderedReviewAsync(ReviewOrderModel model);


        Task CreateCompletedReviewAsync(ReviewCreateModel model);

    }
}
