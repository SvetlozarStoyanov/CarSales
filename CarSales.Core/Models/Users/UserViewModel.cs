using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public Gender Gender { get; set; }
        public decimal Credits { get; set; }
    }
}
