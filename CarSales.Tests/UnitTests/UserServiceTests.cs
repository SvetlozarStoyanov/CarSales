using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Extensions;
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
        public async Task Test_IsUserNameTakenAsync_ReturnsTrue_IfUserNameIsTaken()
        {
            var result = await userService.IsUserNameTakenAsync("b5fef437-f504-46d2-926d-3158e54e1932", "Owner2");
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task Test_IsUserNameTakenAsync_ReturnsFalse_IfUserWithGivenUserIdHasTheSameName()
        {
            var result = await userService.IsUserNameTakenAsync("b5fef437-f504-46d2-926d-3158e54e1932", "Owner");
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task Test_CanEditProfileAsync_ReturnsTrue_IfIdsAreEqual()
        {
            var result = await userService.CanEditProfileAsync("b5fef437-f504-46d2-926d-3158e54e1932", "b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task Test_CanEditProfileAsync_ReturnsFalse_IfIdsAreNotEqual()
        {
            var result = await userService.CanEditProfileAsync("b5fef437-f504-46d2-926d-3158e54e1932", "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d");
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task Test_GetUserAvailableCreditsAsync_ReturnsCorrectCredits_IfUserExists()
        {
            var result = await userService.GetUserAvailableCreditsAsync("9b92fe41-3f2e-4eb1-990b-73c2ea2d746d");
            Assert.That(result, Is.EqualTo(50000m));
        }

        [Test]
        public async Task Test_GetUserByIdAsync_ReturnsCorrectModel()
        {
            var result = await userService.GetUserByIdAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(result.Id, Is.EqualTo("b5fef437-f504-46d2-926d-3158e54e1932"));
            Assert.That(result.ProfilePicture, Is.EqualTo("/img/Profile/MaleProfile.png"));
            Assert.That(result.Gender, Is.EqualTo(Gender.Male));
            Assert.That(result.Credits, Is.EqualTo(50000));
        }
        [Test]
        public async Task Test_GetUserNavbarModelAsync_ReturnsCorrectModel()
        {
            var user = await userService.GetUserByIdAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            var result = await userService.GetUserNavbarModelAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(result.Id, Is.EqualTo(user.Id));
            Assert.That(result.ProfilePicture, Is.EqualTo(user.ProfilePicture));
            Assert.That(result.Gender, Is.EqualTo(user.Gender));
            Assert.That(result.Credits, Is.EqualTo(user.Credits));
        }

        [Test]
        public async Task Test_CreateUserEditModelAsync_ReturnsCorrectModel()
        {
            var user = await userService.GetUserByIdAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            var result = await userService.CreateUserEditModelAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.UserName, Is.EqualTo(user.UserName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.PhoneNumber, Is.EqualTo(user.PhoneNumber));
        }
    }
}
