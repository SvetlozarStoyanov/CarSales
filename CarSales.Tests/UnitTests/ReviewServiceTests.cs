using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Xunit;

namespace CarSales.Tests.UnitTests
{
    public class ReviewServiceTests /*: IAsyncLifetime*/
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IDistributedCache cache;
        private IReviewService reviewService;
        private IContainer container;

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

            container = new ContainerBuilder()
              // Set the image for the container to "testcontainers/helloworld:1.1.0".
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

    }
}
