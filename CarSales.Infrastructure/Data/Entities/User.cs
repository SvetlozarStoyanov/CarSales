using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(3), MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string LastName { get; set; }

        public decimal Credits { get; set; } = 50000;
    }
}
