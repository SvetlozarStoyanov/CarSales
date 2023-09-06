using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Tests.UnitTests
{
    [TestFixture]
    public class VehicleServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IVehicleService vehicleService;

        [SetUp]
        public async Task Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<CarSalesDbContext>()
            .UseInMemoryDatabase("CarSalesTestDB")
            .Options;

            context = new CarSalesDbContext(contextOptions, false);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new Repository(context);

            vehicleService = new VehicleService(repository);
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithNoFilters_ReturnsAllVehicles()
        {
            var vehicles = await vehicleService.GetVehiclesForSaleAsync();
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithSearchTerm_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetVehiclesForSaleAsync("BMW");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetVehiclesForSaleAsync("Audi");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetVehiclesForSaleAsync("Opel");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithSetVehiclesPerPage_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetVehiclesForSaleAsync(null, 2, 1);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(2));
            vehicles = await vehicleService.GetVehiclesForSaleAsync(null, 2, 2);
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithVehicleTypes_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "Car");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            vehicles = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "Motorcycle");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithSorting_ReturnsAppropriateSortedVehicles()
        {
            var vehicles = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            Assert.That(vehicles.Vehicles.First().Name, Is.EqualTo("Bugatti Veyron 2011"));
            vehicles = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "", VehicleSorting.Alphabetically);
            Assert.That(vehicles.Vehicles.First().Name, Is.EqualTo("Audi A6 2014"));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithNoFilters_ReturnsAllImportedVehicles()
        {
            var vehicles = await vehicleService.GetImportedVehiclesAsync();
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithSearchTerm_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetImportedVehiclesAsync("BMW");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            vehicles = await vehicleService.GetImportedVehiclesAsync("Audi");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
            vehicles = await vehicleService.GetImportedVehiclesAsync("Opel");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithSetVehiclesPerPage_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetImportedVehiclesAsync(null, 1, 1);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
            vehicles = await vehicleService.GetImportedVehiclesAsync(null, 1, 2);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithVehicleTypes_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetImportedVehiclesAsync(null, 6, 1, "Car");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            vehicles = await vehicleService.GetImportedVehiclesAsync(null, 6, 1, "Motorcycle");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithSorting_ReturnsAppropriateSortedVehicles()
        {
            var vehicles = await vehicleService.GetImportedVehiclesAsync(null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithNoFilters_ReturnsAllOwnerVehicles()
        {
            var vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithSearchTerm_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", "BMW");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", "Audi");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
            vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", "Opel");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithSetVehiclesPerPage_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 2, 1);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithVehicleTypes_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 6, 1, "Car");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 6, 1, "Motorcycle");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithSorting_ReturnsAppropriateSortedVehicles()
        {
            var vehicles = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithNoFilters_ReturnsAllSalesmanVehicles()
        {
            var vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithSearchTerm_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", "BMW");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", "Audi");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(1));
            vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", "Opel");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithSetVehiclesPerPage_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 2, 1);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(2));
            vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 2, 2);
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithVehicleTypes_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "Car");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "Motorcycle");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithSorting_ReturnsAppropriateSortedVehicles()
        {
            var vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(3));
            Assert.That(vehicles.Vehicles.First().Name, Is.EqualTo("Bugatti Veyron 2011"));
            vehicles = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "", VehicleSorting.Alphabetically);
            Assert.That(vehicles.Vehicles.First().Name, Is.EqualTo("Audi A6 2014"));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithNoFilters_ReturnsAllImporterVehicles()
        {
            var vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithSearchTerm_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", "BMW");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", "Audi");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
            vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", "Opel");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithSetVehiclesPerPage_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 1, 1);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
            vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 1, 2);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            Assert.That(vehicles.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithVehicleTypes_ReturnsAppropriateVehicles()
        {
            var vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 6, 1, "Car");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
            vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 6, 1, "Motorcycle");
            Assert.That(vehicles.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithSorting_ReturnsAppropriateSortedVehicles()
        {
            var vehicles = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(vehicles.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetVehicleById_ReturnsCorrectVehicle_IfIdExists()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(1);
            Assert.That(vehicle.Id, Is.EqualTo(1));
            Assert.That(vehicle.Name, Is.EqualTo("BMW M5"));
            Assert.That(vehicle.VehicleType, Is.EqualTo(VehicleType.Car));
        }

        [Test]
        public async Task Test_GetVehicleById_ReturnsNull_IfIdDoesNotExist()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(0);
            Assert.That(vehicle, Is.EqualTo(null));
        }


        [Test]
        public async Task Test_CreateVehicleSellModel_ReturnsCorrectModel_WhenGivenProperId()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(7);
            var vehicleSellModel = await vehicleService.CreateVehicleSellModelAsync(7);
            Assert.That(vehicleSellModel.Id, Is.EqualTo(7));
            Assert.That(vehicleSellModel.Name, Is.EqualTo(vehicle.Name));
            Assert.That(vehicleSellModel.Description, Is.EqualTo(vehicle.Description));
            Assert.That(vehicleSellModel.Price, Is.EqualTo(vehicle.Price));
            Assert.That(vehicleSellModel.OldPrice, Is.EqualTo(vehicle.Price));
            Assert.That(vehicleSellModel.OwnerUserId, Is.EqualTo("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"));
        }

        [Test]
        public async Task Test_CreateVehicleSellModel_ReturnsNull_WhenGivenNonExistingId()
        {
            var vehicleSellModel = await vehicleService.CreateVehicleSellModelAsync(0);
            Assert.That(vehicleSellModel, Is.EqualTo(null));
        }

        [Test]
        public async Task Test_CreateVehicleImportModel_CreatesProperModel()
        {
            var vehicleImportModel = await vehicleService.CreateVehicleImportModelAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            Assert.That(vehicleImportModel, Is.Not.EqualTo(null));
            Assert.That(vehicleImportModel.ImporterId, Is.EqualTo(1));
            Assert.That(vehicleImportModel.VehicleTypes, Is.EqualTo(Enum.GetValues<VehicleType>().ToHashSet()));
        }
    }
}
