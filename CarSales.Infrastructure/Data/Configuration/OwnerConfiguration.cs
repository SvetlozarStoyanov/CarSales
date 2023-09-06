using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasData(CreateOwners());
        }

        private List<Owner> CreateOwners()
        {
            var owners = new List<Owner>()
            {
                new Owner()
                {
                    Id = 1,
                    UserId = "b5fef437-f504-46d2-926d-3158e54e1932"
                },
                new Owner()
                {
                    Id = 2,
                    UserId = "926bee86-8bbd-43f6-bc1c-9639d43531a4"
                },
                new Owner()
                {
                    Id = 3,
                    UserId = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"
                },
                new Owner()
                {
                    Id = 4,
                    UserId = "10933c11-ac2a-410d-b60a-8b1d97324975"
                },
                new Owner()
                {
                    Id = 5,
                    UserId = "4d693871-c20b-4e9f-8490-1c641b9e3a40"
                },
                new Owner()
                {
                    Id = 6,
                    UserId = "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d"
                },
                new Owner()
                {
                    Id = 7,
                    UserId = "cbed6d2a-e60a-49df-a6e3-982ccd980393"
                }
            };

            return owners;
        }
    }
}
