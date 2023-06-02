using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Core.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Gets <see cref="User"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="User"/></returns>
        Task<User> GetUserByIdAsync(string userId);


        Task<decimal> GetUserAvailableCreditsAsync(string userId);
        Task<decimal> GetUserAvailableCreditsAsync(string userId, int offerId);
    }
}
