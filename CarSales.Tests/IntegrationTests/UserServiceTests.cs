using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Extensions;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace CarSales.Tests.IntegrationTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IUserService userService;
        private IDistributedCache cache;

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

            var redisOptions = new RedisCacheOptions();

            redisOptions.InstanceName = "CarSales_";

            redisOptions.Configuration = "localhost:6379" ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
            cache = new RedisCache(redisOptions);

            userService = new UserService(repository, cache);

        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await cache.ClearCacheAsync();
        }


        [Test]
        public async Task Test_EditUserAsync_EditsUserCorrectly()
        {
            var userBeforeEdit = await userService.GetUserByIdAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(userBeforeEdit.UserName, Is.EqualTo("Owner"));
            Assert.That(userBeforeEdit.FirstName, Is.EqualTo("Owner"));
            Assert.That(userBeforeEdit.LastName, Is.EqualTo("Test"));
            Assert.That(userBeforeEdit.Email, Is.EqualTo("owner@gmail.com"));
            var editModel = await userService.CreateUserEditModelAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            editModel.UserName = "The Owner";
            editModel.FirstName = "David";
            editModel.LastName = "George";
            editModel.Email = "david@gmail.com";
            await userService.EditUserAsync(editModel);
            var userAfterEdit = await userService.GetUserByIdAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(userAfterEdit.UserName, Is.EqualTo(editModel.UserName));
            Assert.That(userAfterEdit.FirstName, Is.EqualTo(editModel.FirstName));
            Assert.That(userAfterEdit.LastName, Is.EqualTo(editModel.LastName));
            Assert.That(userAfterEdit.Email, Is.EqualTo(editModel.Email));
        }
    }
}
