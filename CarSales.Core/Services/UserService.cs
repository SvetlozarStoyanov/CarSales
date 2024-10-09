using CarSales.Core.Constants;
using CarSales.Core.Contracts;
using CarSales.Core.Extensions;
using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CarSales.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDistributedCache cache;

        public UserService(IUnitOfWork repository, IDistributedCache cache)
        {
            this.unitOfWork = repository;
            this.cache = cache;
        }

        public async Task<bool> IsUserNameTakenAsync(string id, string userName)
        {
            var userWithUserName = await unitOfWork.UserRepository.AllReadOnly()
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (userWithUserName != null)
            {
                if (userWithUserName.Id == id)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public async Task<bool> CanEditProfileAsync(string id, string loggedInUserId)
        {
            if (loggedInUserId == id)
            {
                return true;
            }
            return false;
        }

        public async Task<decimal> GetUserAvailableCreditsAsync(string id)
        {
            var owner = await unitOfWork.OwnerRepository.AllReadOnly()
                .Where(o => o.UserId == id)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();

            var availableCredits = owner.User.Credits - owner.Offers
                  .Where(off => off.Status == OfferStatus.Pending)
                  .Sum(off => off.Price);

            return availableCredits;
        }

        public async Task<decimal> GetUserAvailableCreditsAsync(string userId, int offerId)
        {
            var owner = await unitOfWork.OwnerRepository.AllReadOnly()
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();

            var availableCredits = owner.User.Credits - owner.Offers
                .Where(off => off.Status == OfferStatus.Pending && off.Id != offerId)
                .Sum(off => off.Price);

            return availableCredits;
        }
        public async Task<UserNavbarModel> GetUserNavbarModelAsync(string id)
        {

            var recordId = $"{DateTime.Now.ToString(RedisConstants.KeyStringFormat)}_GetUserNavbarModelAsync";

            var userNavbarModel = await cache.GetRecordAsync<UserNavbarModel>(recordId);

            if (userNavbarModel is null || userNavbarModel.Id != id)
            {
                userNavbarModel = await unitOfWork.UserRepository.AllReadOnly()
                    .Where(u => u.Id == id)
                    .Select(u => new UserNavbarModel()
                    {
                        Id = u.Id,
                        Gender = u.Gender,
                        ProfilePicture = u.ImageUrl != null ? u.ImageUrl : $"/img/Profile/{u.Gender}Profile.png",
                        Credits = u.Credits
                    })
                    .FirstOrDefaultAsync();


                await cache.SetRecordAsync<UserNavbarModel>(recordId,
                    userNavbarModel,
                    TimeSpan.FromSeconds(60),
                    TimeSpan.FromSeconds(60));
            }


            return userNavbarModel;
        }
        public async Task<UserViewModel> GetUserByIdAsync(string id)
        {
            var user = await unitOfWork.UserRepository.AllReadOnly()
                .Where(u => u.Id == id)
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ProfilePicture = u.ImageUrl != null ? u.ImageUrl : $"/img/Profile/{u.Gender}Profile.png",
                    Gender = u.Gender,
                    Credits = u.Credits
                })
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEditModel> CreateUserEditModelAsync(string id)
        {
            var user = await unitOfWork.UserRepository.AllReadOnly()
                    .Where(u => u.Id == id)
                    .Select(u => new UserEditModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        ImageUrl = u.ImageUrl,
                        Gender = u.Gender,
                    })
                    .FirstOrDefaultAsync();
            return user;
        }

        public async Task EditUserAsync(UserEditModel model)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(model.Id);

            if (model.ImageUrl == string.Empty)
            {
                model.ImageUrl = null;
            }
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.ImageUrl = model.ImageUrl;
            user.PhoneNumber = model.PhoneNumber;


            await unitOfWork.SaveChangesAsync();
        }

        private UserEditModel CreateUserEditModel(UserViewModel viewModel)
        {
            var editModel = new UserEditModel()
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Gender = viewModel.Gender,
                ImageUrl = viewModel.ProfilePicture
            };

            return editModel;
        }
    }
}
