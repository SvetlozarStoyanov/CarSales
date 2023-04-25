using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Core.Models.Users
{
    public class UserRegisterModel
    {
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

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6), MaxLength(60)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
