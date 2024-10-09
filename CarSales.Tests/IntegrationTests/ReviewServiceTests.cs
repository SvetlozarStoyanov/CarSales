using CarSales.Core.Contracts;
using CarSales.Core.Extensions;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace CarSales.Tests.IntegrationTests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private CarSalesDbContext context;
        private IUnitOfWork unitOfWork;
        private IDistributedCache cache;
        private IReviewService reviewService;
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

            unitOfWork = new UnitOfWork(context);

            var redisOptions = new RedisCacheOptions();
            redisOptions.InstanceName = "CarSales_";

            redisOptions.Configuration = "localhost:6379" ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
            cache = new RedisCache(redisOptions);

            reviewService = new ReviewService(unitOfWork, cache);
            reviewerService = new ReviewerService(unitOfWork);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await cache.ClearCacheAsync();
        }


        [Test]
        public async Task Test_GetRandomReview_ReturnsRandomReview()
        {
            var latestReviews = await reviewService.GetLatestReviewsAsync();

            var randomReview = await reviewService.GetRandomReviewAsync(latestReviews);

            Assert.That(latestReviews.Contains(randomReview), Is.EqualTo(true));
        }

        [Test]
        [Order(1)]
        public async Task Test_CreateOrderedReviewAsync_CreatesOrderedReview()
        {
            var reviewerReviewTypesAndPrices = await reviewerService.GetReviewTypesAndPricesAsync(2);
            var orderModel = await reviewService.CreateReviewOrderModelAsync(2, 2, reviewerReviewTypesAndPrices);
            orderModel.ReviewTypeIndex = 2;
            await reviewService.CreateOrderedReviewAsync("66ccb670-f0dd-4aa1-a83d-8b2a0003bb50", orderModel);
            var result = await unitOfWork.ReviewRepository.AllReadOnly()
                .Where(r => r.ReviewerId == 2 && r.VehicleId == 2)
                .FirstOrDefaultAsync();
            Assert.That(result, Is.Not.EqualTo(null));
            Assert.That(result.ReviewStatus, Is.EqualTo(ReviewStatus.Ordered));
            Assert.That(result.Id, Is.EqualTo(4));
        }
        [Test]
        [Order(2)]

        public async Task Test_CanCreateReviewAsync_ReturnsTrueIfReviewerCanCreateReview()
        {
            var result = await reviewService.CanCreateReviewAsync("9b92fe41-3f2e-4eb1-990b-73c2ea2d746d", 4);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Order(3)]

        public async Task Test_CreateReviewCreateModelAsync_CreatesCorrectReviewCreateModel()
        {
            var result = await reviewService.CreateReviewCreateModelAsync(4);
            Assert.That(result, Is.Not.EqualTo(null));
        }

        [Test]
        [Order(4)]

        public async Task Test_CreateCompletedReviewAsync_CreatesReviewSuccessfully()
        {
            var reviews = await reviewService.GetReviewsAsync();
            var reviewsCountBeforeAddingNewCompletedReview = reviews.ReviewCount;
            var createModel = await reviewService.CreateReviewCreateModelAsync(4);
            createModel.Title = "This is a test text";
            createModel.Overview = "This is a test text";
            createModel.Performance = "This is a test text";
            createModel.Interior = "This is a test text";
            createModel.Longevity = "This is a test text";
            createModel.Features = "This is a test text";
            createModel.Id = 4;
            await reviewService.CreateCompletedReviewAsync(createModel);
            reviews = await reviewService.GetReviewsAsync();
            Assert.That(reviews.ReviewCount, Is.EqualTo(reviewsCountBeforeAddingNewCompletedReview + 1));
        }
    }
}
