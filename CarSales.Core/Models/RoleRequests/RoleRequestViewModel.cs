using CarSales.Core.Models.Users;

namespace CarSales.Core.Models.RoleRequests
{
    public class RoleRequestViewModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public UserWithRolesModel UserModel { get; set; } = null!;
    }
}
