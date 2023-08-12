using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleImportModel
    {
        [Required(ErrorMessage = "Required field")]
        public string Brand { get; set; } = null!;
        [Required(ErrorMessage = "Required field")]
        public string ModelName { get; set; } = null!;
        [Required(ErrorMessage = "Required field")]
        [MinLength(5,ErrorMessage = "Must be more than 5 symbols"), MaxLength(100, ErrorMessage = "Must be less than 100 symbols")]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int YearProduced { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double TopSpeed { get; set; }
        [Required(ErrorMessage = "Required field")]
        public VehicleType VehicleType { get; set; }
        [Required]
        [Range(1.00,9900000.00 ,ErrorMessage = "Range 1 - 9900000")]
        public decimal Price { get; set; }
        public int? ImporterId { get; set; }
        public VehicleType[]? VehicleTypes { get; set; }
    }
}
