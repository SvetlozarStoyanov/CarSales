using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Vehicles
{
    public class VehicleChangeRatingModel
    {
        public int Id { get; set; }
        public VehicleRating OldRating { get; set; }
        public VehicleRating NewRating { get; set; }
    }
}
