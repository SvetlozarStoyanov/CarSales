using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;

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
    }
}
