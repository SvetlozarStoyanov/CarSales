using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Users
{
    public class UserWithRolesModel
    {
        public UserWithRolesModel()
        {
            Roles = new HashSet<string>();
        }
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public Gender Gender { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
