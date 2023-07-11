using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IUserService
    {

        Task<bool> CanEditProfileAsync(string userId, string loggedInUserId);
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

        /// <summary>
        /// Gets <see cref="User"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="UserViewModel"/></returns>
        Task<UserViewModel> GetUserByIdAsync(string userId);

        /// <summary>
        /// Creates a <see cref="UserEditModel"/> from <see cref="User"/> with id given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="UserEditModel"/></returns>
        Task<UserEditModel> CreateUserEditModelAsync(string userId);


        /// <summary>
        /// Edits <see cref="User"/> information with information from <paramref name="model"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditUserAsync(UserEditModel model);
    }
}
