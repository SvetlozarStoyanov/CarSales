using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(CreateUsers());
        }


        private List<User> CreateUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            List<User> users = new List<User>();
            string[] names = { "Admin", "Owner", "Owner2", "Salesman" };
            string[] guids =
                {
                    //Admins
                    "cbed6d2a-e60a-49df-a6e3-982ccd980393",
                    //Owners
                    "b5fef437-f504-46d2-926d-3158e54e1932",
                    "926bee86-8bbd-43f6-bc1c-9639d43531a4",
                    //Salesmen
                    "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50",
                };
            var password = "password";
            int i = 0;
            foreach (string name in names)
            {
                var user = new User()
                {
                    Id = guids[i++],
                    FirstName = name,
                    LastName = "Test",
                    UserName = name,
                    NormalizedUserName = name.ToUpper(),
                    Email = $"{name}@gmail.com",
                    NormalizedEmail = $"{name.ToUpper()}@GMAIL.COM",
                };
                user.PasswordHash = hasher.HashPassword(user, password);
                users.Add(user);
            }
            return users;
        }
        
    }
}
