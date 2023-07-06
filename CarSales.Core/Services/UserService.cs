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
                    PhoneNumber = u.PhoneNumber,
                    Credits = u.Credits
                })
                .FirstOrDefaultAsync();

            user.UserEditModel = CreateUserEditModel(user);

            return user;
        }


        public async Task<decimal> GetUserAvailableCreditsAsync(string userId)
        {
            var owner = await repository.AllReadOnly<Owner>()
                .Where(o => o.UserId == userId)
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
        public async Task EditUserAsync(UserEditModel model)
        {
            var user = await repository.GetByIdAsync<User>(model.Id);

            if (model.UserName != null)
            {
                user.UserName = model.UserName;
            }
            if (model.PhoneNumber != null)
            {
                user.PhoneNumber = model.PhoneNumber;
            }

            await repository.SaveChangesAsync();
        }

        private UserEditModel CreateUserEditModel(UserViewModel viewModel)
        {
            var editModel = new UserEditModel()
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                PhoneNumber = viewModel.PhoneNumber
            };

            return editModel;
        }
    }
}
