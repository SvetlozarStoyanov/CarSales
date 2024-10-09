using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;

namespace CarSales.Core.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork unitOfWork;
        public OwnerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateOwnerAsync(string userId)
        {
            var owner = new Owner() 
            {
                UserId = userId,
            };
            await unitOfWork.OwnerRepository.AddAsync(owner);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<string> GetOwnerUserIdAsync(int id)
        {
            var owner = await unitOfWork.OwnerRepository.GetByIdAsync(id);

            return owner.UserId;
        }
    }
}
