using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Required field")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
