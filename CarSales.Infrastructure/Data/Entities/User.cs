using CarSales.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Notifications = new HashSet<Notification>();
        }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(3), MaxLength(30)]
        public string LastName { get; set; } = null!;

        public Gender Gender { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Credits { get; set; } = 50000;
        public ICollection<Notification> Notifications { get; set; }
    }
}
