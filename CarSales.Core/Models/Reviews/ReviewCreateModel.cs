﻿
using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewCreateModel
    {
        public int Id { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string Title { get; set; }
        public string Overview { get; set; } = null!;
        [MinLength(10), MaxLength(1000)]
        public string Performance { get; set; } = null!;
        [MinLength(10), MaxLength(1000)]
        public string? Interior { get; set; } = null!;
        [MinLength(10), MaxLength(1000)]
        public string? Longevity { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string? Features { get; set; }
        public ReviewType ReviewType { get; set; }
        public decimal Price { get; set; }
        public int ReviewerId { get; set; }
        public int VehicleId { get; set; }
    }
}