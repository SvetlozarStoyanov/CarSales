namespace CarSales.Core.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Gender { get; set; }
        public decimal Credits { get; set; }
        public UserEditModel UserEditModel { get; set; } = null!;
    }
}
