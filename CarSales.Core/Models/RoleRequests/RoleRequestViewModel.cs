﻿namespace CarSales.Core.Models.RoleRequests
{
    public class RoleRequestViewModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<string> UserRoles { get; set; }
    }
}
