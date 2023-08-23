
using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewCreateModel
    {
        public ReviewCreateModel()
        {
            VehicleRatings = Enum.GetValues<VehicleRating>().Skip(1).ToHashSet();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [MinLength(10, ErrorMessage = "Must be above 10 symbols"), MaxLength(100, ErrorMessage = "Must be below 100 symbols")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Required")]
        [MinLength(10, ErrorMessage = "Must be above 10 symbols"), MaxLength(1000, ErrorMessage = "Must be below 1000 symbols")]
        public string Overview { get; set; } = null!;
        [Required(ErrorMessage = "Required")]
        [MinLength(10, ErrorMessage = "Must be above 10 symbols"), MaxLength(1000, ErrorMessage = "Must be below 1000 symbols")]
        public string Performance { get; set; } = null!;
        [Required(ErrorMessage = "Required")]
        [MinLength(10, ErrorMessage = "Must be above 10 symbols"), MaxLength(1000, ErrorMessage = "Must be below 1000 symbols")]
        public string Interior { get; set; } = null!;
        [MinLength(10, ErrorMessage = "Must be above 10 symbols"), MaxLength(1000, ErrorMessage = "Must be below 1000 symbols")]
        public string? Longevity { get; set; }
        [MinLength(10, ErrorMessage = "Must be above 10 symbols"), MaxLength(1000, ErrorMessage = "Must be below 1000 symbols")]
        public string? Features { get; set; }
        public ReviewType ReviewType { get; set; }
        public VehicleRating VehicleRating { get; set; }
        public string VehicleName { get; set; } = null!;
        public ReviewPreviewModel? ReviewPreviewModel { get; set; }
        public ICollection<VehicleRating> VehicleRatings { get; set; }
    }
}
