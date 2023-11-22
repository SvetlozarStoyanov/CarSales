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
                    SalesmanId = null,
                    OwnerId = 3,
                    ImporterId = 1,
                    VehicleId = 1,
                    SalePrice = 10000,
                    VehiclePrice = 10000
                },
                new Sale()
                {
                    Id = 2,
                    SalesmanId = 1,
                    OwnerId = 3,
                    ImporterId = null,
                    VehicleId = 1,
                    SalePrice = 0,
                    VehiclePrice = 10000
                },
                new Sale()
                {
                    Id = 3,
                    SalesmanId = null,
                    OwnerId = 3,
                    ImporterId = 1,
                    VehicleId = 2,
                    SalePrice = 60000,
                    VehiclePrice = 60000
                },
                new Sale()
                {
                    Id = 4,
                    SalesmanId = 1,
                    OwnerId = 3,
                    ImporterId = null,
                    VehicleId = 2,
                    SalePrice = 0,
                    VehiclePrice = 60000
                },
                new Sale()
                {
                    Id = 5,
                    SalesmanId = null,
                    OwnerId = 3,
                    ImporterId = 1,
                    VehicleId = 3,
                    SalePrice = 20000,
                    VehiclePrice = 20000
                },
                new Sale()
                {
                    Id = 6,
                    SalesmanId = 1,
                    OwnerId = 3,
                    ImporterId = null,
                    VehicleId = 3,
                    SalePrice = 0,
                    VehiclePrice = 20000
                },
                new Sale()
                {
                    Id = 7,
                    SalesmanId = null,
                    OwnerId = 3,
                    ImporterId = 1,
                    VehicleId = 6,
                    SalePrice = 18000,
                    VehiclePrice = 18000
                },
                new Sale()
                {
                    Id = 8,
                    SalesmanId = 1,
                    OwnerId = 3,
                    ImporterId = null,
                    VehicleId = 6,
                    SalePrice = 0,
                    VehiclePrice = 18000
                    
                },
                new Sale()
                {
                    Id = 9,
                    SalesmanId = null,
                    OwnerId = 3,
                    ImporterId = 1,
                    VehicleId = 7,
                    SalePrice = 8000,
                    VehiclePrice = 8000
                },
            };
            return sales;
        }
    }
}
