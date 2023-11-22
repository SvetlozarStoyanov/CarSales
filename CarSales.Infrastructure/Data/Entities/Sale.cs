using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal SalePrice { get; set; }
        public decimal VehiclePrice { get; set; }

        [ForeignKey(nameof(Entities.Salesman.Id))]
        public int? SalesmanId { get; set; }
        public Salesman Salesman { get; set; }

        [ForeignKey(nameof(Entities.Owner.Id))]
        public int? OwnerId { get; set; }
        public Owner Owner { get; set; }

        [ForeignKey(nameof(Entities.Vehicle.Id))]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int? ImporterId { get; set; }
        public Importer Importer { get; set; }
    }
}
