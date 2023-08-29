using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CarSales.Tests.UnitTests
{
    public class VehicleServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IDistributedCache cache;
        private IVehicleService vehicleService;

        [SetUp]
        public async Task Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<CarSalesDbContext>()
            .UseInMemoryDatabase("CarSalesTestDB")
            .Options;

            context = new CarSalesDbContext(contextOptions, false);

            context.ApplyConfiguration = false;

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new Repository(context);

            vehicleService = new VehicleService(repository);
        }

        [Test]
        public async Task Test_GetVehiclesForSaleWithNoFiltersReturnsAllVehicles()
        {
            var vehicles = await vehicleService.GetAllVehiclesForSaleAsync();
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
        }
        [Test]
        public async Task Test_GetVehiclesForSaleWithSearchTermReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetAllVehiclesForSaleAsync("BMW");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetAllVehiclesForSaleAsync("Audi");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetAllVehiclesForSaleAsync("Opel");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetVehiclesForSaleWithSetVehiclesPerPageReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetAllVehiclesForSaleAsync(null, 2, 1);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(2));
            vehicles = await vehicleService.GetAllVehiclesForSaleAsync(null, 2, 2);
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetVehiclesForSaleWithVehicleTypesReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetAllVehiclesForSaleAsync(null, 6, 1, "Car");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            vehicles = await vehicleService.GetAllVehiclesForSaleAsync(null, 6, 1, "Motorcycle");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetVehiclesForSaleWithSortingReturnsAppropriateSortedVehicles()
        {
            var vehicles = await vehicleService.GetAllVehiclesForSaleAsync(null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            Assert.That(vehicles.Vehicles.First().Name, Is.EqualTo("Bugatti Veyron 2011"));
            vehicles = await vehicleService.GetAllVehiclesForSaleAsync(null, 6, 1, "", VehicleSorting.Alphabetically);
            Assert.That(vehicles.Vehicles.First().Name, Is.EqualTo("Audi A6 2014"));
        }
    }
}
