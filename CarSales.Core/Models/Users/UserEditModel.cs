using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserEditModel
    {
        public string Id { get; set; } = null!;
        [MinLength(3), MaxLength(30)]
        public string? UserName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
