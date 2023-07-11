using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        public UserService(IRepository repository)
        {
            this.repository = repository;
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
            var owner = await repository.AllReadOnly<Owner>()
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
            var owner = await repository.AllReadOnly<Owner>()
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();

            var availableCredits = owner.User.Credits - owner.Offers
                .Where(off => off.Status == OfferStatus.Pending && off.Id != offerId)
                .Sum(off => off.Price);

            return availableCredits;
        }

        public async Task<UserViewModel> GetUserByIdAsync(string id)
        {
            var user = await repository.AllReadOnly<User>()
                .Where(u => u.Id == id)
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ImageUrl = u.ImageUrl,
                    Gender = u.Gender,
                    Credits = u.Credits
                })
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEditModel> CreateUserEditModelAsync(string id)
        {
            var user = await repository.AllReadOnly<User>()
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
            var user = await repository.GetByIdAsync<User>(model.Id);

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


            await repository.SaveChangesAsync();
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
                ImageUrl = viewModel.ImageUrl
            };

            return editModel;
        }


    }
}
