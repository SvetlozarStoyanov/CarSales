using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasData(CreateSales());
        }

        private List<Sale> CreateSales()
        {
            var sales = new List<Sale>()
            {
                new Sale()
                {
                    Id = 1,
                    SalesmanId = 1,
                    OwnerId = null,
                    ImporterId = 1,
                    VehicleId = 1,
                    SalePrice = 10000
                },
                new Sale()
                {
                    Id = 2,
                    SalesmanId = 1,
                    OwnerId = 1,
                    ImporterId = null,
                    VehicleId = 3,
                    SalePrice = 20000
                }
            };
            return sales;
        }
    }
}
