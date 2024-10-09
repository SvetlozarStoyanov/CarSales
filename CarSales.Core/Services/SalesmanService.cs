using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class SalesmanService : ISalesmanService
    {
        private readonly IUnitOfWork unitOfWork;
        public SalesmanService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task CreateOrRenewSalesmanAsync(string userId)
        {
            var salesman = await unitOfWork.SalesmanRepository.All()
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
                await unitOfWork.SalesmanRepository.AddAsync(salesman);
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task RetireSalesmanAsync(string userId)
        {
            var salesman = await unitOfWork.SalesmanRepository.All()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (salesman != null)
            {
                salesman.IsActive = false;
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<string> GetSalesmanUserIdAsync(int id)
        {
            var salesman = await unitOfWork.SalesmanRepository.GetByIdAsync(id);
            return salesman.UserId;
        }
    }
}
