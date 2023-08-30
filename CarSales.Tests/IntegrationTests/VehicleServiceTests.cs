using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Exceptions;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Entities;
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
                .Where(s => s.SalesmanId == 1 && s.OwnerId == 1)
                .FirstOrDefaultAsync();

            Assert.That(sale, Is.Not.EqualTo(null));
            Assert.That(sale.SalePrice, Is.Not.EqualTo(vehicle.Price));
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
            var vehicle = await vehicleService.GetVehicleByIdAsync(5);

            Assert.That(async () => await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932"), Throws.Exception.TypeOf<NotForSaleException>());
            var exception = Assert.ThrowsAsync<NotForSaleException>(async () => await vehicleService.BuyVehicleFromSalesmanAsync(vehicle.Id, "b5fef437-f504-46d2-926d-3158e54e1932"));
            Assert.That(exception.Message, Is.EqualTo("Item is not for sale!"));
        }
    }
}
