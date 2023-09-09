using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Tests.IntegrationTests
{
    public class ReviewerServiceTests
    {
        private const string REVIEWER_USER_ID = "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d";
        private CarSalesDbContext context;
        private IRepository repository;
        private IReviewerService reviewerService;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<CarSalesDbContext>()
            .UseInMemoryDatabase("CarSalesTestDB")
            .Options;

            context = new CarSalesDbContext(contextOptions, false);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new Repository(context);

            reviewerService = new ReviewerService(repository);
        }

        [Test]
        public async Task Test_CreateReviewerPriceEditModelAsync_ReturnsCorrectModel_IfIdExists()
        {
            var result = await reviewerService.CreateReviewerPriceEditModelAsync(REVIEWER_USER_ID);
            var reviewer = await repository.GetByIdAsync<Reviewer>(2);
            Assert.That(result.ShortReviewPrice,Is.EqualTo(reviewer.ShortReviewPrice));
            Assert.That(result.StandartReviewPrice,Is.EqualTo(reviewer.StandartReviewPrice));
            Assert.That(result.PremiumReviewPrice,Is.EqualTo(reviewer.PremiumReviewPrice));
        }

        [Test]
        public async Task Test_CreateReviewerPriceEditModelAsync_ReturnsNull_IfIdDoesNotExist()
        {
            var result = await reviewerService.CreateReviewerPriceEditModelAsync("invalidId");
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_EditReviewPricesAsync_EditsReviewPricesCorrectly()
        {
            var editModel = await reviewerService.CreateReviewerPriceEditModelAsync(REVIEWER_USER_ID);
            editModel.ShortReviewPrice = 99;
            editModel.StandartReviewPrice = 101;
            editModel.PremiumReviewPrice = 102;
            var reviewer = await repository.GetByIdAsync<Reviewer>(2);
            Assert.That(reviewer.ShortReviewPrice, Is.Not.EqualTo(editModel.ShortReviewPrice));
            Assert.That(reviewer.StandartReviewPrice, Is.Not.EqualTo(editModel.StandartReviewPrice));
            Assert.That(reviewer.PremiumReviewPrice, Is.Not.EqualTo(editModel.PremiumReviewPrice));
            await reviewerService.EditReviewPricesAsync(editModel);
            reviewer = await repository.GetByIdAsync<Reviewer>(2);
            Assert.That(reviewer.ShortReviewPrice, Is.EqualTo(editModel.ShortReviewPrice));
            Assert.That(reviewer.StandartReviewPrice, Is.EqualTo(editModel.StandartReviewPrice));
            Assert.That(reviewer.PremiumReviewPrice, Is.EqualTo(editModel.PremiumReviewPrice));
        }
    }
}
