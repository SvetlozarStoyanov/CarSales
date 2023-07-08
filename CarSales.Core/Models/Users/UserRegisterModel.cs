using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserRegisterModel
    {
        public UserRegisterModel()
        {
            Genders = Enum.GetValues<Gender>().ToHashSet();
        }
        [Required]
        [MinLength(2), MaxLength(60)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(2), MaxLength(60)]
        public string LastName { get; set; } = null!;

        [Required]
        [MinLength(5), MaxLength(60)]
        public string Username { get; set; } = null!;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public Gender Gender { get; set; }
        public string? ImageUrl { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6), MaxLength(60)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        public virtual ICollection<Gender> Genders { get; set; }
    }
}
