﻿using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Offers
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public OfferStatus Status { get; set; }
        public int VehicleId { get; set; }
        public VehicleMinModel Vehicle { get; set; } = null!;
        public int OfferorId { get; set; }
        public string OfferorName { get; set; } = null!;
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; } = null!;
        public OfferEditModel? OfferEditModel { get; set; }
    }
}
