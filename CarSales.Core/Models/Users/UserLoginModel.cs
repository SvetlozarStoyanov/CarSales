using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Users
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
