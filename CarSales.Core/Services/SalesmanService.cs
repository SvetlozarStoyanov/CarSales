using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class SalesmanService : ISalesmanService
    {
        private readonly IRepository repository;
        public SalesmanService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task CreateOrRenewSalesmanAsync(string userId)
        {
            var salesman = await repository.All<Salesman>()
                .FirstOrDefaultAsync(s => s.UserId == userId);
            if (salesman != null)
            {
                salesman.IsActive = true;
            }
            else
            {
                salesman = new Salesman()
                {
                    UserId = userId,
                    SalesmanRating = SalesmanRating.Average
                };
                await repository.AddAsync<Salesman>(salesman);
            }
            await repository.SaveChangesAsync();
        }

        public async Task RetireSalesmanAsync(string userId)
        {
            var salesman = await repository.All<Salesman>()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (salesman != null)
            {
                salesman.IsActive = false;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<string> GetSalesmanUserIdAsync(int id)
        {
            var salesman = await repository.GetByIdAsync<Salesman>(id);
            return salesman.UserId;
        }
    }
}
