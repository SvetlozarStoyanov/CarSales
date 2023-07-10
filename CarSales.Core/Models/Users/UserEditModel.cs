using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserEditModel
    {
        public string Id { get; set; } = null!;
        [MinLength(3), MaxLength(30)]
        public string FirstName { get; set; } = null!;
        [MinLength(3), MaxLength(30)]
        public string LastName { get; set; } = null!;
        [MinLength(3), MaxLength(30)]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Phone]
        public string? PhoneNumber { get; set; }
        [Url]
        public string? ImageUrl { get; set; }
        public decimal? Credits { get; set; }
    }
}
