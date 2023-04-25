using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasData(CreateVehicles());
        }

        private List<Vehicle> CreateVehicles()
        {

            var vehicles = new List<Vehicle>()
            {
                new Vehicle()
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "M5",
                    Description = "Fast car",
                    TopSpeed = 250,
                    KilometersDriven = 0,
                    Price = 9000,
                    SalesmanId = 1,
                    OwnerId = null,
                    VehicleType = VehicleType.Car,
                    VehicleRating = VehicleRating.Reliable,
                    YearProduced = 2016
                },
                new Vehicle()
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "M3",
                    Description = "Classic car",
                    TopSpeed = 240,
                    KilometersDriven = 0,
                    Price = 5000,
                    SalesmanId = 1,
                    OwnerId = null,
                    VehicleType = VehicleType.Car,
                    VehicleRating = VehicleRating.Reliable,
                    YearProduced = 2004
                },
                new Vehicle()
                {
                    Id = 3,
                    Brand = "BMW",
                    Model = "X5",
                    Description = "Popular car",
                    TopSpeed = 243,
                    KilometersDriven = 2000,
                    Price = 18000,
                    SalesmanId = null,
                    OwnerId = 1,
                    VehicleType = VehicleType.Car,
                    VehicleRating = VehicleRating.Good,
                    YearProduced = 2020
                }
            };
            return vehicles;
        }
    }
}
