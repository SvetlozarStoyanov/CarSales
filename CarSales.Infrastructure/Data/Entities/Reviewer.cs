using System.ComponentModel.DataAnnotations;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Reviewer
    {
        public Reviewer()
        {
            Reviews = new List<Review>();
        }

        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public decimal ShortReviewPrice { get; set; }
        public decimal StandartReviewPrice { get; set; }
        public decimal PremiumReviewPrice { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
