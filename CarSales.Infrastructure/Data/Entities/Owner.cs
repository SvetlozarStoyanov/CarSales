using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Owner
    {
        public Owner()
        {
            Vehicles = new HashSet<Vehicle>();
            Sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Entities.User.Id))]
        public string UserId { get; set; } = null!;
        public virtual User User { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
