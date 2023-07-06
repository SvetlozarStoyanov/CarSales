namespace CarSales.Core.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public decimal Credits { get; set; }
        public UserEditModel UserEditModel { get; set; }
    }
}
