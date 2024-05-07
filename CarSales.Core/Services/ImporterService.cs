using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class ImporterService : IImporterService
    {
        private readonly IRepository repository;
        public ImporterService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateOrRenewImporterAsync(string userId)
        {
            var importer = await repository.All<Importer>()
                .Where(i => i.UserId == userId)
                .FirstOrDefaultAsync();

            if (importer == null)
            {
                importer = new Importer()
                {
                    UserId = userId,
                    ImporterRating = ImporterRating.Average,
                };
                await repository.AddAsync<Importer>(importer);
            }
            else
            {
                importer.IsActive = true;
            }
            await repository.SaveChangesAsync();
        }

        public async Task RetireImporterAsync(string userId)
        {
            var importer = await repository.All<Importer>()
                .Where(i => i.UserId == userId)
                .FirstOrDefaultAsync();
            importer.IsActive = false;
            await repository.SaveChangesAsync();
        }

        public async Task<string> GetImporterUserIdAsync(int id)
        {
            var importer = await repository.GetByIdAsync<Importer>(id);
            return importer.UserId;
        }
    }
}
