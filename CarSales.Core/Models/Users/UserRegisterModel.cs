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
        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Length in range 2-60")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Length in range 2-60")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Required field")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Length in range 5-60")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Must be valid email")]
        public string Email { get; set; } = null!;
        public Gender Gender { get; set; }
        [Url(ErrorMessage = "Must be valid URL")]
        public string? ImageUrl { get; set; }
        [Phone(ErrorMessage = "Incorrect format")]
        [RegularExpression("[0-9]{3}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Incorrect format")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Length in range 6-60")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        public virtual ICollection<Gender> Genders { get; set; }
    }
}
