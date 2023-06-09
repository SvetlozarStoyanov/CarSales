﻿using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Salesman
    {
        public Salesman()
        {
            Vehicles = new HashSet<Vehicle>();
            Sales = new HashSet<Sale>();
            Offers = new HashSet<Offer>();
        }

        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey(nameof(Entities.User.Id))]
        public string UserId { get; set; } = null!;
        public virtual User User { get; set; }
        public SalesmanRating SalesmanRating { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }

    }
}
