using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
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
        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await repository.GetByIdAsync<User>(userId);
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
    }
}
