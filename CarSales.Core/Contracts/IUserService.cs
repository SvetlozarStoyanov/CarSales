using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using StackExchange.Redis;

namespace CarSales.Core.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Returns true if <see cref="User"/> with different <see cref="User"/>.Id <paramref name="userName"/> exists,
        /// returns false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> IsUserNameTakenAsync(string id, string userName);

        /// <summary>
        /// Returns true if <paramref name="userId"/> is equal to <paramref name="loggedInUserId"/>,
        /// returns false otherwise
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loggedInUserId"></param>
        /// <returns><see cref="bool"/></returns>
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
        /// Creates a <see cref="UserNavbarModel"/> for <see cref="User"/> with
        /// given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserNavbarModel> GetUserNavbarModelAsync(string id);

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
