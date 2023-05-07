using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Vehicle
    {
        public Vehicle()
        {
            Sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; } = null!;
        [Required]
        public string Model { get; set; } = null!;
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int YearProduced { get; set; }
        public double TopSpeed { get; set; }
        public double KilometersDriven { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public decimal Price { get; set; }

        [ForeignKey(nameof(Entities.Owner.Id))]
        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }

        [ForeignKey(nameof(Entities.Salesman.Id))]
        public int? SalesmanId { get; set; }
        public Salesman? Salesman { get; set; }
        public int? ImporterId { get; set; }
        public Importer? Importer { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
