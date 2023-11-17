using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required] 
        public string Link { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(User.Id))]
        public string UserId { get; set; } = null!;
        public bool IsRead { get; set; } = false;
    }
}
