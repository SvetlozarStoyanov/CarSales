using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSales.Infrastructure.Data.Entities
{
    public class RoleRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Entities.Role.Id))]
        public string RoleId { get; set; }
        public Role Role { get; set; }
        [ForeignKey(nameof(Entities.User.Id))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
