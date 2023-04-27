using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            };

            return owners;
        }
    }
}
