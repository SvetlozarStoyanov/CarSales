using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Enums;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace CarSales.Tests.UnitTests
{
    public class ReviewServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IDistributedCache cache;
        private IReviewService reviewService;
        private IContainer container;

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

            container = new ContainerBuilder()
              // Set the image for the container
              .WithImage("redis:latest")
              // Bind port 6379 of the container to a port 6379 on the host.
              .WithPortBinding(6379, 6379)
              // Wait until the HTTP endpoint of the container is available.
              .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
              // Build the container configuration.
              .Build();

            await container.StartAsync();
            var redisOptions = new RedisCacheOptions();

            redisOptions.InstanceName = "CarSales_";

            redisOptions.Configuration = "localhost:6379" ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
            cache = new RedisCache(redisOptions);

            reviewService = new ReviewService(repository, cache);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await container.StopAsync();
        }

        [Test]
        public async Task Test_GetLatestReviewsAsync_ReturnsCorrectQuantityOfReviews()
        {
            var reviews = await reviewService.GetLatestReviewsAsync();
            Assert.That(reviews.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetReviewsAsync_WithNoFilters_ReturnsAllReviews()
        {
            var result = await reviewService.GetReviewsAsync();
            Assert.That(result.ReviewCount, Is.EqualTo(3));
        }
        [Test]
        public async Task Test_GetReviewsAsync_WithSearchTerm_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewsAsync("Rocket-ship");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewsAsync("Bad car");
            Assert.That(result.ReviewCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetReviewsAsync_WithSetVehicleName_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewsAsync(null, "BMW M5");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewsAsync(null, "Bugatti Veyron");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewsAsync(null, "Audi A6");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetReviewsAsync_WithSetReviewsPerPage_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewsAsync(null, null, 2, 1);
            Assert.That(result.ReviewCount, Is.EqualTo(3));
            Assert.That(result.Reviews.Count, Is.EqualTo(2));
            result = await reviewService.GetReviewsAsync(null, null, 2, 2);
            Assert.That(result.ReviewCount, Is.EqualTo(3));
            Assert.That(result.Reviews.Count, Is.EqualTo(1));

        }

        [Test]
        public async Task Test_GetReviewsAsync_WithSetVehicleTypes_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewsAsync(null, null, 6, 1, "", "Car");
            Assert.That(result.ReviewCount, Is.EqualTo(3));
            result = await reviewService.GetReviewsAsync(null, null, 6, 1, "", "Motorcycle");
            Assert.That(result.ReviewCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetReviews_WithSetReviewTypes_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewsAsync(null, null, 6, 1, "Short");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewsAsync(null, null, 6, 1, "Premium");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewsAsync(null, null, 6, 1, "Standart");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewsAsync(null, null, 6, 1, "Short;Standart;Premium");
            Assert.That(result.ReviewCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetReviewsAsync_WithSorting_ReturnsAppropriateSortedReviews()
        {
            var result = await reviewService.GetReviewsAsync(null, null, 6, 1, null, null, ReviewSorting.VehiclePriceDescending);
            Assert.That(result.Reviews.ElementAt(0).VehicleName, Is.EqualTo("Bugatti Veyron"));
            Assert.That(result.Reviews.ElementAt(1).VehicleName, Is.EqualTo("Audi A6"));
            Assert.That(result.Reviews.ElementAt(2).VehicleName, Is.EqualTo("BMW M5"));
            result = await reviewService.GetReviewsAsync(null, null, 1, 1, null, null, ReviewSorting.VehiclePriceDescending);
            Assert.That(result.Reviews.First().VehicleName, Is.EqualTo("Bugatti Veyron"));
            result = await reviewService.GetReviewsAsync(null, null, 1, 2, null, null, ReviewSorting.VehiclePriceDescending);

            Assert.That(result.Reviews.First().VehicleName, Is.EqualTo("Audi A6"));
            result = await reviewService.GetReviewsAsync(null, null, 1, 3, null, null, ReviewSorting.VehiclePriceDescending);
            Assert.That(result.Reviews.First().VehicleName, Is.EqualTo("BMW M5"));
            Assert.That(result.ReviewCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetReviewersReviewsAsync_WithNoFilters_ReturnsAllReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40");
            Assert.That(result.ReviewCount, Is.EqualTo(3));
        }
        [Test]
        public async Task Test_GetReviewersReviewsAsync_WithSearchTerm_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", "Rocket-ship");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", "Bad car");
            Assert.That(result.ReviewCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetReviewersReviewsAsync_WithSetVehicleName_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, "BMW M5");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, "Bugatti Veyron");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, "Audi A6");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetReviewersReviewsAsync_WithSetReviewsPerPage_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 2, 1);
            Assert.That(result.ReviewCount, Is.EqualTo(3));
            Assert.That(result.Reviews.Count, Is.EqualTo(2));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 2, 2);
            Assert.That(result.ReviewCount, Is.EqualTo(3));
            Assert.That(result.Reviews.Count, Is.EqualTo(1));

        }

        [Test]
        public async Task Test_GetReviewersReviewsAsync_WithSetVehicleTypes_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, "", "Car");
            Assert.That(result.ReviewCount, Is.EqualTo(3));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, "", "Motorcycle");
            Assert.That(result.ReviewCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetReviewersReviews_WithSetReviewTypes_ReturnsCorrectReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, "Short");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, "Premium");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, "Standart");
            Assert.That(result.ReviewCount, Is.EqualTo(1));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, "Short;Standart;Premium");
            Assert.That(result.ReviewCount, Is.EqualTo(3));
        }

        [Test]
        public async Task Test_GetReviewersReviewsAsync_WithSorting_ReturnsAppropriateSortedReviews()
        {
            var result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 6, 1, null, null, ReviewStatus.Completed, ReviewSorting.VehiclePriceDescending);
            Assert.That(result.Reviews.ElementAt(0).VehicleName, Is.EqualTo("Bugatti Veyron"));
            Assert.That(result.Reviews.ElementAt(1).VehicleName, Is.EqualTo("Audi A6"));
            Assert.That(result.Reviews.ElementAt(2).VehicleName, Is.EqualTo("BMW M5"));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 1, 1, null, null, ReviewStatus.Completed, ReviewSorting.VehiclePriceDescending);
            Assert.That(result.Reviews.First().VehicleName, Is.EqualTo("Bugatti Veyron"));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 1, 2, null, null, ReviewStatus.Completed, ReviewSorting.VehiclePriceDescending);

            Assert.That(result.Reviews.First().VehicleName, Is.EqualTo("Audi A6"));
            result = await reviewService.GetReviewerReviewsAsync("4d693871-c20b-4e9f-8490-1c641b9e3a40", null, null, 1, 3, null, null, ReviewStatus.Completed, ReviewSorting.VehiclePriceDescending);
            Assert.That(result.Reviews.First().VehicleName, Is.EqualTo("BMW M5"));
            Assert.That(result.ReviewCount, Is.EqualTo(3));
        }
    }
}
