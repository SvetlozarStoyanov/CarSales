using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class ReviewerService : IReviewerService
    {
        private readonly IRepository repository;
        public ReviewerService(IRepository repository) 
        {
            this.repository = repository;
        }

        public async Task CreateOrRenewReviewerAsync(string userId)
        {
            var reviewer = await repository.All<Reviewer>()
                .FirstOrDefaultAsync(s => s.UserId == userId);
            if (reviewer != null)
            {
                reviewer.IsActive = true;
            }
            else
            {
                reviewer = new Reviewer()
                {
                    UserId = userId,
                    //SalesmanRating = SalesmanRating.Average
                };
                await repository.AddAsync<Reviewer>(reviewer);
            }
            await repository.SaveChangesAsync();
        }

        public async Task RetireReviewerAsync(string userId)
        {
            var reviewer = await repository.All<Reviewer>()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (reviewer != null)
            {
                reviewer.IsActive = false;
                await repository.SaveChangesAsync();
            }
        }
    }
}
