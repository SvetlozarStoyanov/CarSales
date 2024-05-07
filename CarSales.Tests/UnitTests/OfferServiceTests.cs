using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Exceptions;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Tests.UnitTests
{
    [TestFixture]
    public class OfferServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IOfferService offerService;
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

            offerService = new OfferService(repository);
            vehicleService = new VehicleService(repository);
        }

        [Test]
        public async Task Test_CreateOfferCreateModelAsync_IfGivenExistingId_CreatesModelSuccessfully()
        {
            var vehicle = await vehicleService.GetVehicleByIdAsync(1);
            var result = await offerService.CreateOfferCreateModelAsync("b5fef437-f504-46d2-926d-3158e54e1932", 1);
            Assert.That(result.InitialPrice, Is.EqualTo(vehicle.Price));
            Assert.That(result.SalesmanId, Is.EqualTo(vehicle.SalesmanId));
        }

        [Test]
        public async Task Test_CreateOfferCreateModelAsync_IfGivenNonExistingId_ThrowsException()
        {
            Assert.ThrowsAsync<NullReferenceException>(() => offerService.CreateOfferCreateModelAsync("b5fef437-f504-46d2-926d-3158e54e1932", 0));
        }

        [Test]
        public async Task Test_CreateOfferCreateModelAsync_IfGivenVehicleWhichIsNotForSale_ThrowsException()
        {
            Assert.ThrowsAsync<NotForSaleException>(() => offerService.CreateOfferCreateModelAsync("b5fef437-f504-46d2-926d-3158e54e1932", 4));
        }


        [Test]
        public async Task Test_CanCreateOfferAsync_ReturnsTrueIfUserCanCreateOffer()
        {
            var result = await offerService.CanCreateOfferAsync("b5fef437-f504-46d2-926d-3158e54e1932", 2);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task Test_CanCreateOfferAsync_ReturnsFalseIfUserCannotCreateOffer()
        {
            var result = await offerService.CanCreateOfferAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", 2);
            Assert.That(result, Is.EqualTo(false));
        }
    }
}
