using CarSales.Infrastructure.Data.DataAccess.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;

namespace CarSales.Core.Services
{
    public class ImporterService : IImporterService
    {
        private readonly IUnitOfWork unitOfWork;
        public ImporterService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateOrRenewImporterAsync(string userId)
        {
            var importer = await unitOfWork.ImporterRepository.All()
                .Where(i => i.UserId == userId)
                .FirstOrDefaultAsync();

            if (importer == null)
            {
                importer = new Importer()
                {
                    UserId = userId,
                    ImporterRating = ImporterRating.Average,
                };
                await unitOfWork.ImporterRepository.AddAsync(importer);
            }
            else
            {
                importer.IsActive = true;
            }

            await unitOfWork.SaveChangesAsync();
        }

        public async Task RetireImporterAsync(string userId)
        {
            var importer = await unitOfWork.ImporterRepository.All()
                .Where(i => i.UserId == userId)
                .FirstOrDefaultAsync();

            importer.IsActive = false;

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<string> GetImporterUserIdAsync(int id)
        {
            var importer = await unitOfWork.ImporterRepository.GetByIdAsync(id);
            return importer.UserId;
        }
    }
}
