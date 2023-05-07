using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(CreateUserRoles());
        }

        private List<IdentityUserRole<string>> CreateUserRoles()
        {
            var usersAndRoles = new List<IdentityUserRole<string>>()
            {
                //Admins
                new IdentityUserRole<string>()
                {
                    UserId = "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                    RoleId = "dacb7d40-e742-435c-b131-145300f3c97d"
                },
                //Owners
                new IdentityUserRole<string>()
                {
                    UserId = "b5fef437-f504-46d2-926d-3158e54e1932",
                    RoleId = "bbea2448-c801-43d1-8b05-e3a2c22338d9"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                    RoleId = "bbea2448-c801-43d1-8b05-e3a2c22338d9"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                    RoleId = "bbea2448-c801-43d1-8b05-e3a2c22338d9"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "10933c11-ac2a-410d-b60a-8b1d97324975",
                    RoleId = "bbea2448-c801-43d1-8b05-e3a2c22338d9"
                },
                //Salesmen
                new IdentityUserRole<string>()
                {
                    UserId = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                    RoleId = "c63016c0-e087-43dc-bb9c-a8958a05cbdd"
                },
                //Importers
                new IdentityUserRole<string>()
                {
                    UserId = "10933c11-ac2a-410d-b60a-8b1d97324975",
                    RoleId = "9cbd5531-0c49-4889-95b9-b81fc1e7653a"
                }
            };

            return usersAndRoles;
        }
    }
}
