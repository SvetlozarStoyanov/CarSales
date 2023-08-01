using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Users
{
    public class UserNavbarModel
    {
        public string Id { get; set; } = null!;
        public Gender Gender { get; set; }
        public string ProfilePicture { get; set; } = null!;
        public decimal Credits { get; set; }
    }
}
