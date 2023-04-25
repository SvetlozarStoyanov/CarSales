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
                //Admins
                "dacb7d40-e742-435c-b131-145300f3c97d",
                //Owners
                "bbea2448-c801-43d1-8b05-e3a2c22338d9",
                //Salesmen
                "c63016c0-e087-43dc-bb9c-a8958a05cbdd"
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
                    Description = "Can buy cars."
                },
                new Role()
                {
                    Id = roleGuids[i],
                    ConcurrencyStamp = roleGuids[i++],
                    Name = "Salesman",
                    NormalizedName = "SALESMAN",
                    Description = "Can buy and sell cars."
                },
            };


            return roles;
        }
    }
}
