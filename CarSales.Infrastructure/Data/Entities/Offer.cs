using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public OfferStatus Status { get; set; }

        [ForeignKey(nameof(Entities.Vehicle.Id))]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [ForeignKey(nameof(Entities.Owner.Id))]
        public int OfferorId { get; set; }
        public Owner Offeror { get; set; }

        [ForeignKey(nameof(Entities.Salesman.Id))]
        public int SalesmanId { get; set; }
        public Salesman Salesman { get; set; }
    }
}
