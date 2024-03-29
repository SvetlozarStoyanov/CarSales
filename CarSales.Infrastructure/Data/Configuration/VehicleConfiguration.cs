﻿    using CarSales.Infrastructure.Data.Entities;
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
                    ImageUrl = "https://media.ed.edmunds-media.com/bmw/m5/2021/oem/2021_bmw_m5_sedan_base_fq_oem_8_815.jpg",
                    TopSpeed = 250,
                    KilometersDriven = 0,
                    Price = 9000,
                    SalesmanId = 1,
                    OwnerId = null,
                    ImporterId = null,
                    VehicleType = VehicleType.Car,
                    YearProduced = 2016
                },
                new Vehicle()
                {
                    Id = 2,
                    Brand = "Bugatti",
                    Model = "Veyron",
                    Description = "Fast sports car",
                    ImageUrl = "https://i.ytimg.com/vi/rvn4lHrr6AQ/maxresdefault.jpg",
                    TopSpeed = 350,
                    KilometersDriven = 0,
                    Price = 60000,
                    SalesmanId = 1,
                    OwnerId = null,
                    ImporterId = null,
                    VehicleType = VehicleType.Car,
                    YearProduced = 2011
                },
                new Vehicle()
                {
                    Id = 3,
                    Brand = "Audi",
                    Model = "A6",
                    Description = "Fast modern car",
                    ImageUrl = "https://cache1.24chasa.bg/Images/Cache/889/IMAGE_13981889_40_0.jpg",
                    TopSpeed = 300,
                    KilometersDriven = 0,
                    Price = 20000,
                    SalesmanId = 1,
                    OwnerId = null,
                    ImporterId = null,
                    VehicleType = VehicleType.Car,
                    YearProduced = 2014
                },
                new Vehicle()
                {
                    Id = 4,
                    Brand = "BMW",
                    Model = "M3",
                    Description = "Fast modern car",
                    ImageUrl = "https://media.ed.edmunds-media.com/bmw/m3/2022/oem/2022_bmw_m3_sedan_competition_fq_oem_1_1600.jpg",
                    TopSpeed = 240,
                    KilometersDriven = 0,
                    Price = 10000,
                    SalesmanId = null,
                    OwnerId = null,
                    ImporterId = 1,
                    VehicleType = VehicleType.Car,
                    YearProduced = 2004
                },
                new Vehicle()
                {
                    Id = 5,
                    Brand = "BMW",
                    Model = "X7",
                    Description = "Fast new car",
                    ImageUrl = "https://stimg.cardekho.com/images/carexteriorimages/930x620/BMW/X7/10571/1689673096346/front-left-side-47.jpg",
                    TopSpeed = 300,
                    KilometersDriven = 0,
                    Price = 70000,
                    SalesmanId = null,
                    OwnerId = null,
                    ImporterId = 1,
                    VehicleType = VehicleType.Car,
                    YearProduced = 2023
                },
                new Vehicle()
                {
                    Id = 6,
                    Brand = "BMW",
                    Model = "X5",
                    Description = "Popular car",
                    ImageUrl = "https://media.ed.edmunds-media.com/bmw/x5/2024/oem/2024_bmw_x5_4dr-suv_xdrive50e_fq_oem_1_1600.jpg",
                    TopSpeed = 243,
                    KilometersDriven = 2000,
                    Price = 18000,
                    SalesmanId = null,
                    OwnerId = 1,
                    VehicleType = VehicleType.Car,
                    YearProduced = 2020
                },
                new Vehicle()
                {
                    Id = 7,
                    Brand = "Ford",
                    Model = "Escort",
                    Description = "Old car",
                    ImageUrl = "https://media.autoexpress.co.uk/image/private/s--X-WVjvBW--/f_auto,t_content-image-full-desktop@1/v1562246965/autoexpress/2018/10/_dsf9821.jpg",
                    TopSpeed = 160,
                    KilometersDriven = 20000,
                    Price = 8000,
                    SalesmanId = null,
                    OwnerId = 3,
                    VehicleType = VehicleType.Car,
                    YearProduced = 1968
                }
            };
            return vehicles;
        }
    }
}
