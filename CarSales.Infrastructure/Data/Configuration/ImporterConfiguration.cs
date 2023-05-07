using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class ImporterConfiguration : IEntityTypeConfiguration<Importer>
    {
        public void Configure(EntityTypeBuilder<Importer> builder)
        {
            builder.HasData(CreateImporters());
        }

        private List<Importer> CreateImporters()
        {
            var importers = new List<Importer>()
            {
                new Importer()
                {
                    Id = 1,
                    UserId = "10933c11-ac2a-410d-b60a-8b1d97324975",
                    ImporterRating = ImporterRating.Reliable
                }
            };
            return importers;
        }
    }
}
