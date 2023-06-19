using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(CreateRoles());
        }

        private List<Role> CreateRoles()
        {
            var roleGuids = new string[]
            {
                //Admin
                "dacb7d40-e742-435c-b131-145300f3c97d",
                //Owner
                "bbea2448-c801-43d1-8b05-e3a2c22338d9",
                //Salesman
                "c63016c0-e087-43dc-bb9c-a8958a05cbdd",
                //Importer
                "9cbd5531-0c49-4889-95b9-b81fc1e7653a",
                //Reviewer
                "2d1b88f7-208a-43de-bb14-aca56b43080c"
            };
            int i = 0;
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = roleGuids[i],
                    ConcurrencyStamp = roleGuids[i++],
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "Admin"
                },
                new Role()
                {
                    Id = roleGuids[i],
                    ConcurrencyStamp = roleGuids[i++],
                    Name = "Owner",
                    NormalizedName = "OWNER",
                    Description = "Can buy vehicles."
                },
                new Role()
                {
                    Id = roleGuids[i],
                    ConcurrencyStamp = roleGuids[i++],
                    Name = "Salesman",
                    NormalizedName = "SALESMAN",
                    Description = "Can buy and sell vehicles."
                },
                new Role()
                {
                    Id = roleGuids[i],
                    ConcurrencyStamp = roleGuids[i++],
                    Name = "Importer",
                    NormalizedName = "IMPORTER",
                    Description = "Can buy and import vehicles."
                },
                new Role()
                {
                    Id = roleGuids[i],
                    ConcurrencyStamp = roleGuids[i++],
                    Name = "Reviewer",
                    NormalizedName = "REVIEWER",
                    Description = "Can buy and review vehicles."
                }
            };

            return roles;
        }
    }
}
