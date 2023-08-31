using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Exceptions;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CarSales.Tests.IntegrationTests
{
    public class VehicleServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IDistributedCache cache;
        private IVehicleService vehicleService;
        private IUserService userService;

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
            userService = new UserService(repository, cache);
        }

        [Test]
        public async Task Test_BuyVehicleFromSalesman_CorrectlyBuysVehicleAndTransfersCredits_IfBuyerHasEnoughCredits()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(1);
            var buyerCredits = await userService.GetUserAvailableCreditsAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            var vehicleSalesmanCredits = await userService.GetUserAvailableCreditsAsync(vehicle.SalesmanUserId);
            Assert.That(vehicle.SalesmanId, Is.EqualTo(1));
            Assert.That(vehicle.OwnerId, Is.EqualTo(null));
            Assert.That(buyerCredits, Is.EqualTo(50000));
            Assert.That(vehicleSalesmanCredits, Is.EqualTo(50000));

            await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932");
            buyerCredits = await userService.GetUserAvailableCreditsAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            vehicleSalesmanCredits = await userService.GetUserAvailableCreditsAsync(vehicle.SalesmanUserId);
            vehicle = await vehicleService.GetVehicleByIdAsync(1);

            Assert.That(buyerCredits, Is.EqualTo(50000 - vehicle.Price));
            Assert.That(vehicleSalesmanCredits, Is.EqualTo(50000 + vehicle.Price));
            Assert.That(vehicle.SalesmanId, Is.EqualTo(null));
            Assert.That(vehicle.OwnerId, Is.EqualTo(1));
            var sale = await repository.AllReadOnly<Sale>()
                .Where(s => s.SalesmanId == 1
                    && s.OwnerId == 1
                    && s.VehicleId == vehicle.Id)
                .FirstOrDefaultAsync();

            Assert.That(sale, Is.Not.EqualTo(null));
            Assert.That(sale.SalePrice, Is.EqualTo(vehicle.Price));
        }

        [Test]
        public async Task Test_BuyVehicleFromSalesman_ThrowsException_IfBuyerDoesNotHaveEnoughCredits()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(2);

            Assert.That(async () => await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932"), Throws.Exception.TypeOf<InsufficientCreditsException>());
            var exception = Assert.ThrowsAsync<InsufficientCreditsException>(async () => await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932"));
            Assert.That(exception.Message, Is.EqualTo("You do not have enough credits to purchase this item!"));
        }

        [Test]
        public async Task Test_BuyFromSalesman_ThrowsException_IfVehicleIsNotForSale()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(6);

            Assert.That(async () => await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932"), Throws.Exception.TypeOf<NotForSaleException>());
            var exception = Assert.ThrowsAsync<NotForSaleException>(async () => await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932"));
            Assert.That(exception.Message, Is.EqualTo("Item is not for sale!"));
        }

        [Test]
        public async Task Test_BuyVehicleFromImporter_CorrectlyBuysVehicleAndTransfersCredits_IfBuyerHasEnoughCredits()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(4);
            var buyerCredits = await userService.GetUserAvailableCreditsAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");
            var vehicleImporterCredits = await userService.GetUserAvailableCreditsAsync(vehicle.ImporterUserId);
            Assert.That(vehicle.ImporterId, Is.EqualTo(1));
            Assert.That(vehicle.OwnerId, Is.EqualTo(null));
            Assert.That(buyerCredits, Is.EqualTo(50000));
            Assert.That(vehicleImporterCredits, Is.EqualTo(50000));

            await vehicleService.BuyVehicleFromImporterAsync(vehicle.Id, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");
            buyerCredits = await userService.GetUserAvailableCreditsAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");
            vehicleImporterCredits = await userService.GetUserAvailableCreditsAsync(vehicle.ImporterUserId);
            vehicle = await vehicleService.GetVehicleByIdAsync(4);

            Assert.That(buyerCredits, Is.EqualTo(50000 - vehicle.Price));
            Assert.That(vehicleImporterCredits, Is.EqualTo(50000 + vehicle.Price));
            Assert.That(vehicle.ImporterId, Is.EqualTo(null));
            Assert.That(vehicle.OwnerId, Is.EqualTo(3));
            var sale = await repository.AllReadOnly<Sale>()
                .Where(s => s.ImporterId == 1
                    && s.OwnerId == 3
                    && s.VehicleId == vehicle.Id)
                .FirstOrDefaultAsync();

            Assert.That(sale, Is.Not.EqualTo(null));
            Assert.That(sale.SalePrice, Is.EqualTo(vehicle.Price));
        }

        [Test]
        public async Task Test_BuyVehicleFromImporter_ThrowsException_IfBuyerDoesNotHaveEnoughCredits()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(5);

            Assert.That(async () => await vehicleService.BuyVehicleFromImporterAsync(vehicle.Id, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"), Throws.Exception.TypeOf<InsufficientCreditsException>());
            var exception = Assert.ThrowsAsync<InsufficientCreditsException>(async () => await vehicleService.BuyVehicleFromImporterAsync(vehicle.Id, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"));
            Assert.That(exception.Message, Is.EqualTo("You do not have enough credits to purchase this item!"));
        }

        [Test]
        public async Task Test_BuyVehicleFromImporter_ThrowsException_IfVehicleIsNotForSale()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(1);

            Assert.That(async () => await vehicleService.BuyVehicleFromImporterAsync(vehicle.Id, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"), Throws.Exception.TypeOf<NotForSaleException>());
            var exception = Assert.ThrowsAsync<NotForSaleException>(async () => await vehicleService.BuyVehicleFromImporterAsync(vehicle.Id, "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50"));
            Assert.That(exception.Message, Is.EqualTo("Item is not for sale!"));
        }

        [Test]
        public async Task Test_PutVehicleForSale_PutsVehicleOnSale()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(7);
            Assert.That(vehicle.SalesmanId, Is.EqualTo(null));
            Assert.That(vehicle.OwnerId, Is.Not.EqualTo(null));
            await vehicleService.PutVehicleForSaleAsync(vehicle.VehicleSellModel);
            vehicle = await vehicleService.GetVehicleByIdAsync(7);
            Assert.That(vehicle.SalesmanId, Is.Not.EqualTo(null));
            Assert.That(vehicle.OwnerId, Is.EqualTo(null));
        }

        [Test]
        public async Task Test_EditVehicle_EditsVehicle()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(4);
            vehicle.VehicleEditModel.Price = vehicle.Price - 1;
            var newPrice = vehicle.VehicleEditModel.Price;
            vehicle.VehicleEditModel.Description = vehicle.Description + " edited!";
            var newDescription = vehicle.VehicleEditModel.Description;
            await vehicleService.EditVehicleAsync(vehicle.VehicleEditModel);
            vehicle = await vehicleService.GetVehicleByIdAsync(4);
            Assert.That(vehicle.Price, Is.EqualTo(newPrice));
            Assert.That(vehicle.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public async Task Test_ImportVehicle_ImportsVehicle()
        {
            var vehicleImportModel = await vehicleService.CreateVehicleImportModelAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            var importerCredits = await userService.GetUserAvailableCreditsAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            var importerCreditsBeforeImporting = importerCredits;
            var importedVehicles = await vehicleService.GetImportedVehiclesAsync();
            var importedVehiclesCountBeforeImporting = importedVehicles.VehicleCount;
            Assert.That(importerCredits, Is.EqualTo(50000));
            vehicleImportModel.Brand = "BMW";
            vehicleImportModel.ModelName = "X6";
            vehicleImportModel.Price = 4000m;
            vehicleImportModel.YearProduced = 2012;
            vehicleImportModel.TopSpeed = 300;
            vehicleImportModel.Description = "BMW car";
            vehicleImportModel.VehicleType = VehicleType.Car;
            await vehicleService.ImportVehicleAsync(vehicleImportModel);
            importedVehicles = await vehicleService.GetImportedVehiclesAsync();
            Assert.That(importedVehicles.VehicleCount, Is.EqualTo(importedVehiclesCountBeforeImporting + 1));
            importerCredits = await userService.GetUserAvailableCreditsAsync("10933c11-ac2a-410d-b60a-8b1d97324975");
            Assert.That(importerCredits, Is.EqualTo(importerCreditsBeforeImporting - vehicleImportModel.Price * 0.9m));

            Assert.That(importedVehicles.Vehicles.Any(v => v.Name == $"{vehicleImportModel.Brand} {vehicleImportModel.ModelName} {vehicleImportModel.YearProduced}"
            && v.Price == vehicleImportModel.Price));
        }
    }
}
