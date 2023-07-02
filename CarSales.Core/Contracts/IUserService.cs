using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

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

        /// <summary>
        /// Returns the <see cref="User.Credits"/> of <see cref="User"/>
        /// with <paramref name="userId"/> taking into account all <see cref="Offer"/>s the <see cref="User"/>
        /// has made (still <see cref="OfferStatus.Pending"/>)
        /// </summary> 
        /// <param name="userId"></param>
        /// <returns><see cref="decimal"/></returns>
        Task<decimal> GetUserAvailableCreditsAsync(string userId);

        /// <summary>
        /// Returns the <see cref="User.Credits"/> of <see cref="User"/>
        /// with <paramref name="userId"/> taking into account all <see cref="Offer"/>s the <see cref="User"/>
        /// has made (still <see cref="OfferStatus.Pending"/>) excluding the <see cref="Offer"/> with <paramref name="offerId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="offerId"></param>
        /// <returns><see cref="decimal"/></returns>
        Task<decimal> GetUserAvailableCreditsAsync(string userId, int offerId);
    }
}
