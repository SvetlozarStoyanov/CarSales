using CarSales.Core.Contracts;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Tests.IntegrationTests
{
    [TestFixture]
    public class OfferServiceTests
    {
        private CarSalesDbContext context;
        private IUnitOfWork unitOfWork;
        private IOfferService offerService;
        private IUserService userService;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<CarSalesDbContext>()
            .UseInMemoryDatabase("CarSalesTestDB")
            .Options;

            context = new CarSalesDbContext(contextOptions, false);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            unitOfWork = new UnitOfWork(context);

            offerService = new OfferService(unitOfWork);
            userService = new UserService(unitOfWork, null);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [Test]
        [Order(1)]
        public async Task Test_CreateOfferAsync_IfGivenExistingId_CreatesOfferSuccessfully()
        {
            for (int i = 0; i < 3; i++)
            {
                var offerCreateModel = await offerService.CreateOfferCreateModelAsync("b5fef437-f504-46d2-926d-3158e54e1932", i + 1);
                offerCreateModel.Description = "I want this car";
                offerCreateModel.Price = 1000;

                await offerService.CreateOfferAsync(offerCreateModel);
            }

            var ownerOffers = await offerService.GetOwnerOffersAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            var salesmanOffers = await offerService.GetSalesmanOffersAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");

            Assert.That(ownerOffers.OfferCount, Is.EqualTo(3));
            Assert.That(salesmanOffers.OfferCount, Is.EqualTo(3));
        }

        [Test]
        [Order(2)]
        public async Task Test_GetOfferByIdAsync_IfGivenIdExists_ReturnsCorrectOffer()
        {
            var result = await offerService.GetOfferByIdAsync(1);
            Assert.That(result.Price, Is.EqualTo(1000));
            Assert.That(result.Description, Is.EqualTo("I want this car"));
        }

        [Test]
        [Order(3)]
        public async Task Test_GetOfferByIdAsync_IfGivenIdDoesNotExist_ReturnsNull()
        {
            var result = await offerService.GetOfferByIdAsync(0);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        [Order(4)]
        public async Task Test_CanEditOfferAsync_ReturnsTrueIfUserCanEditOffer()
        {
            var result = await offerService.CanEditOfferAsync("b5fef437-f504-46d2-926d-3158e54e1932", 1);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Order(5)]
        public async Task Test_CanEditOfferAsync_ReturnsFalseIfUserCannotEditOffer()
        {
            var result = await offerService.CanEditOfferAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", 1);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Order(6)]
        public async Task Test_CanViewOfferAsync_ReturnsTrueIfUserCanViewffer()
        {
            //Check if Owner who sent the offer can view it
            var result = await offerService.CanViewOfferAsync("b5fef437-f504-46d2-926d-3158e54e1932", 1);
            Assert.That(result, Is.EqualTo(true));
            //Check if Salesman who received the offer can view it
            result = await offerService.CanViewOfferAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", 1);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Order(7)]
        public async Task Test_CanCanViewOfferAsync_ReturnsFalseIfUserCannotViewOffer()
        {
            var result = await offerService.CanViewOfferAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", 1);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Order(8)]
        public async Task Test_CanRespondToOfferAsync_ReturnsTrueIfUserCanRespondToOffer()
        {

           var result = await offerService.CanRespondToOfferAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", 1);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Order(9)]
        public async Task Test_CanRespondToOfferAsync_ReturnsFalseIfUserCannotRespondToOffer()
        {
            var result = await offerService.CanRespondToOfferAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", 1);
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Order(10)]
        public async Task Test_GetOfferIdAsync_ReturnsCorrectId()
        {
            var result = await offerService.GetOfferIdAsync("b5fef437-f504-46d2-926d-3158e54e1932", 1);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [Order(11)]
        public async Task Test_CreateOfferEditModelAsync_CreatesModelSuccessfully()
        {
            var result = await offerService.CreateOfferEditModelAsync(1);
            Assert.That(result.Price, Is.EqualTo(1000));
            Assert.That(result.Description, Is.EqualTo("I want this car"));
        }

        [Test]
        [Order(12)]
        public async Task Test_EditOfferAsync_SuccessfullyEditsOffer()
        {
            var offerBeforeEdit = await offerService.GetOfferByIdAsync(1);
            Assert.That(offerBeforeEdit.Price, Is.EqualTo(1000));
            var offerEditModel = await offerService.CreateOfferEditModelAsync(1);
            offerEditModel.Description = "Edited offer description";
            offerEditModel.Price = 1200;
            await offerService.EditOfferAsync(offerEditModel);
            var offerAfterEdit = await offerService.GetOfferByIdAsync(1);
            Assert.That(offerAfterEdit.Description, Is.Not.EqualTo(offerBeforeEdit.Description));
            Assert.That(offerAfterEdit.Price, Is.EqualTo(offerEditModel.Price));
        }

        [Test]
        [Order(13)]
        public async Task Test_DeclineOfferAsync_SuccessfullyDeclinesOffer()
        {
            var offer = await offerService.GetOfferByIdAsync(1);
            Assert.That(offer.Status, Is.EqualTo(OfferStatus.Pending));
            await offerService.DeclineOfferAsync(1);
            offer = await offerService.GetOfferByIdAsync(1);
            Assert.That(offer.Status, Is.EqualTo(OfferStatus.Declined));
        }

        [Test]
        [Order(14)]
        public async Task Test_CancelOfferAsync_SuccessfullyDeletesOffer()
        {
            await offerService.CancelOfferAsync(2);
            var offer = await offerService.GetOfferByIdAsync(2);
            Assert.That(offer, Is.EqualTo(null));
        }

        [Test]
        [Order(15)]
        public async Task Test_AcceptOfferAsync_SuccessfullyAcceptsOfferAndTransfersCredits()
        {
            var offer = await offerService.GetOfferByIdAsync(3);
            Assert.That(offer.Price, Is.EqualTo(1000));
            Assert.That(offer.Status, Is.EqualTo(OfferStatus.Pending));
            await offerService.AcceptOfferAsync(3);
            offer = await offerService.GetOfferByIdAsync(3);
            Assert.That(offer.Status, Is.EqualTo(OfferStatus.Accepted));
            var ownerCreditsAfterOfferIsAccepted = await userService.GetUserAvailableCreditsAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            var salesmanCreditsAfterOfferIsAccepted = await userService.GetUserAvailableCreditsAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50");
            Assert.That(ownerCreditsAfterOfferIsAccepted, Is.EqualTo(50000 - offer.Price));
            Assert.That(salesmanCreditsAfterOfferIsAccepted, Is.EqualTo(50000 + offer.Price));
        }
    }
}
