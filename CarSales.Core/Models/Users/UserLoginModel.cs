using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserLoginModel
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
