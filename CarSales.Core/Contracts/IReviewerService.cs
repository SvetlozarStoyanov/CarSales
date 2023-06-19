
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Core.Contracts
{
    public interface IReviewerService
    {
        /// <summary>
        /// Creates a new <see cref="Reviewer"/> with given <paramref name="userId"/>,
        /// If <see cref="Reviewer"/> already exists ot sets <see cref="Reviewer.IsActive"/> to true
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
    }
}
