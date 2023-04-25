using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class SalesmanConfiguration : IEntityTypeConfiguration<Salesman>
    {
        public void Configure(EntityTypeBuilder<Salesman> builder)
        {
            builder.HasData(CreateSalesmen());
        }

        private List<Salesman> CreateSalesmen()
        {
            var salesmen = new List<Salesman>() 
            { 
                new Salesman()
                {
                    Id = 1,
                    UserId = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                    SalesmanRating = SalesmanRating.Reliable
                }
            };


            return salesmen;
        }
    }
}
