using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserEditModel
    {
        public string Id { get; set; } = null!;
        [Required]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long"), MaxLength(30, ErrorMessage = "First name must be less than 30 characters long")]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(3, ErrorMessage = "Last name Must be at least 3 characters long"), MaxLength(30, ErrorMessage = "Last name must be less than 30 characters long")]
        public string LastName { get; set; } = null!;
        [Required]
        [MinLength(3, ErrorMessage = "Username be at least 3 characters long"), MaxLength(30, ErrorMessage = "Username must be less than 30 characters long")]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Phone]
        public string? PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        [Url]
        public string? ImageUrl { get; set; }
    }
}
