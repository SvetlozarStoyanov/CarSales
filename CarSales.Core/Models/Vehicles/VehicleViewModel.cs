using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int YearProduced { get; set; }
        public double TopSpeed { get; set; }
        public double KilometersDriven { get; set; }
        public decimal Price { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public int? OwnerId { get; set; }
        public string? OwnerUserId { get; set; }
        public string? OwnerName { get; set; }
        public int? SalesmanId { get; set; }
        public string? SalesmanName { get; set; }
    }
}
