using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Core.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IRepository repository;
        public OwnerService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateOwnerAsync(string userId)
        {
            var owner = new Owner() 
            {
                UserId = userId,
            };
            await repository.AddAsync<Owner>(owner);
            await repository.SaveChangesAsync();
        }

        public async Task<string> GetOwnerUserIdAsync(int id)
        {
            var owner = await repository.GetByIdAsync<Owner>(id);

            return owner.UserId;
        }
    }
}
