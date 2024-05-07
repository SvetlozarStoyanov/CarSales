using CarSales.Infrastructure.Data.Common.Repository;
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
        public async Task Test_GetVehiclesForSale_WithNoFilters_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetVehiclesForSaleAsync();
            Assert.That(result.VehicleCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithSearchTerm_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetVehiclesForSaleAsync("BMW");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            result = await vehicleService.GetVehiclesForSaleAsync("Audi");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            result = await vehicleService.GetVehiclesForSaleAsync("Opel");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithSetVehiclesPerPage_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetVehiclesForSaleAsync(null, 2, 1);
            Assert.That(result.VehicleCount, Is.EqualTo(3));
            Assert.That(result.Vehicles.Count, Is.EqualTo(2));
            result = await vehicleService.GetVehiclesForSaleAsync(null, 2, 2);
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithVehicleTypes_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "Car");
            Assert.That(result.VehicleCount, Is.EqualTo(3));
            result = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "Motorcycle");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetVehiclesForSale_WithSorting_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(result.VehicleCount, Is.EqualTo(3));
            Assert.That(result.Vehicles.First().Name, Is.EqualTo("Bugatti Veyron 2011"));
            result = await vehicleService.GetVehiclesForSaleAsync(null, 6, 1, "", VehicleSorting.Alphabetically);
            Assert.That(result.Vehicles.First().Name, Is.EqualTo("Audi A6 2014"));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithNoFilters_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImportedVehiclesAsync();
            Assert.That(result.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithSearchTerm_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImportedVehiclesAsync("BMW");
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            result = await vehicleService.GetImportedVehiclesAsync("Audi");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
            result = await vehicleService.GetImportedVehiclesAsync("Opel");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithSetVehiclesPerPage_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImportedVehiclesAsync(null, 1, 1);
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
            result = await vehicleService.GetImportedVehiclesAsync(null, 1, 2);
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithVehicleTypes_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImportedVehiclesAsync(null, 6, 1, "Car");
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            result = await vehicleService.GetImportedVehiclesAsync(null, 6, 1, "Motorcycle");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImportedVehicles_WithSorting_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImportedVehiclesAsync(null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(result.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithNoFilters_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithSearchTerm_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", "BMW");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", "Audi");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
            result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", "Opel");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithSetVehiclesPerPage_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 2, 1);
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithVehicleTypes_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 6, 1, "Car");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 6, 1, "Motorcycle");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetOwnerVehicles_WithSorting_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetOwnerVehiclesAsync("b5fef437-f504-46d2-926d-3158e54e1932", null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(result.VehicleCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithNoFilters_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");
            Assert.That(result.VehicleCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithSearchTerm_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", "BMW");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", "Audi");
            Assert.That(result.VehicleCount, Is.EqualTo(1));
            result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", "Opel");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithSetVehiclesPerPage_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 2, 1);
            Assert.That(result.VehicleCount, Is.EqualTo(3));
            Assert.That(result.Vehicles.Count, Is.EqualTo(2));
            result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 2, 2);
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
            Assert.That(result.VehicleCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithVehicleTypes_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "Car");
            Assert.That(result.VehicleCount, Is.EqualTo(3));
            result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "Motorcycle");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetSalesmanVehicles_WithSorting_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(result.VehicleCount, Is.EqualTo(3));
            Assert.That(result.Vehicles.First().Name, Is.EqualTo("Bugatti Veyron 2011"));
            result = await vehicleService.GetSalesmanVehiclesAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", null, 6, 1, "", VehicleSorting.Alphabetically);
            Assert.That(result.Vehicles.First().Name, Is.EqualTo("Audi A6 2014"));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithNoFilters_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            Assert.That(result.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithSearchTerm_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", "BMW");
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", "Audi");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
            result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", "Opel");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithSetVehiclesPerPage_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 1, 1);
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
            result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 1, 2);
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            Assert.That(result.Vehicles.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithVehicleTypes_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 6, 1, "Car");
            Assert.That(result.VehicleCount, Is.EqualTo(2));
            result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 6, 1, "Motorcycle");
            Assert.That(result.VehicleCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetImporterVehicles_WithSorting_ReturnsCorrectVehicles()
        {
            var result = await vehicleService.GetImporterVehiclesAsync("10933c11-ac2a-410d-b60a-8b1d97324975", null, 6, 1, "", VehicleSorting.RatingAscending);
            Assert.That(result.VehicleCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetVehicleById_ReturnsCorrectVehicle_IfIdExists()
        {
            var result = await vehicleService.GetVehicleByIdAsync(1);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("BMW M5"));
            Assert.That(result.VehicleType, Is.EqualTo(VehicleType.Car));
        }

        [Test]
        public async Task Test_GetVehicleById_ReturnsNull_IfIdDoesNotExist()
        {
            var result = await vehicleService.GetVehicleByIdAsync(0);
            Assert.That(result, Is.EqualTo(null));
        }


        [Test]
        public async Task Test_CreateVehicleSellModel_ReturnsCorrectModel_IfIdExists()
        {
            var result = await vehicleService.GetVehicleByIdAsync(7);
            var vehicleSellModel = await vehicleService.CreateVehicleSellModelAsync(7);
            Assert.That(vehicleSellModel.Id, Is.EqualTo(7));
            Assert.That(vehicleSellModel.Name, Is.EqualTo(result.Name));
            Assert.That(vehicleSellModel.Description, Is.EqualTo(result.Description));
            Assert.That(vehicleSellModel.Price, Is.EqualTo(result.Price));
            Assert.That(vehicleSellModel.OldPrice, Is.EqualTo(result.Price));
            Assert.That(vehicleSellModel.OwnerUserId, Is.EqualTo("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"));
        }

        [Test]
        public async Task Test_CreateVehicleSellModel_ReturnsNull_IfIdDoesNotExist()
        {
            var result = await vehicleService.CreateVehicleSellModelAsync(0);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task Test_CreateVehicleImportModel_CreatesCorrectModel()
        {
            var result = await vehicleService.CreateVehicleImportModelAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            Assert.That(result, Is.Not.EqualTo(null));
            Assert.That(result.ImporterId, Is.EqualTo(1));
            Assert.That(result.VehicleTypes, Is.EqualTo(Enum.GetValues<VehicleType>().ToHashSet()));
        }
    }
}
