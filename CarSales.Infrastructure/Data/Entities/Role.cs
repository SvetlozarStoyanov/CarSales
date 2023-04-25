using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Role : IdentityRole
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;
    }
}
