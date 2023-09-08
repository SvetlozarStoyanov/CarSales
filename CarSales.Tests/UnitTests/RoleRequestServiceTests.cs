using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Tests.UnitTests
{
    public class RoleRequestServiceTests
    {
        private CarSalesDbContext context;
        private IRepository repository;
        private IRoleRequestService roleRequestService;

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

            roleRequestService = new RoleRequestService(repository);
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {

        }

        [Test]
        [Order(1)]
        public async Task Test_CreateRoleRequestAsync_CreatesRoleRequestSuccessfully()
        {
            await roleRequestService.CreateRoleRequestAsync("c63016c0-e087-43dc-bb9c-a8958a05cbdd", "b5fef437-f504-46d2-926d-3158e54e1932");
            await roleRequestService.CreateRoleRequestAsync("9cbd5531-0c49-4889-95b9-b81fc1e7653a", "926bee86-8bbd-43f6-bc1c-9639d43531a4");
            var roleRequestsCount = repository.AllReadOnly<RoleRequest>().Count();
            Assert.That(roleRequestsCount, Is.EqualTo(2));
        }

        [Test]
        [Order(2)]
        public async Task Test_GetRoleRequestsAsync_WithNoFilters_ReturnsAllRoleRequests()
        {
            var result = await roleRequestService.GetRoleRequestsAsync();
            Assert.That(result.RoleRequestCount, Is.EqualTo(2));
        }

        [Test]
        [Order(3)]
        public async Task Test_GetRoleRequestsAsync_WithSearchTerm_ReturnsCorrectRoleRequests()
        {
            var result = await roleRequestService.GetRoleRequestsAsync("Owner2");
            Assert.That(result.RoleRequestCount, Is.EqualTo(1));
            result = await roleRequestService.GetRoleRequestsAsync("Salesman");
            Assert.That(result.RoleRequestCount, Is.EqualTo(1));
            result = await roleRequestService.GetRoleRequestsAsync("owner");
            Assert.That(result.RoleRequestCount, Is.EqualTo(2));
        }

        [Test]
        [Order(4)]
        public async Task Test_GetRoleRequestsAsync_WithSetRoleRequestsPerPage_ReturnsCorrectRoleRequests()
        {
            var result = await roleRequestService.GetRoleRequestsAsync(null, 1, 1);
            Assert.That(result.RoleRequestCount, Is.EqualTo(2));
            Assert.That(result.RoleRequests.Count, Is.EqualTo(1));
            result = await roleRequestService.GetRoleRequestsAsync(null, 2, 1);
            Assert.That(result.RoleRequestCount, Is.EqualTo(2));
            Assert.That(result.RoleRequests.Count, Is.EqualTo(1));
        }

        [Test]
        [Order(5)]
        public async Task Test_GetRoleRequestsAsync_WithSetRoleNames_ReturnsCorrectRoleRequests()
        {
            var result = await roleRequestService.GetRoleRequestsAsync(null, 6, 1, "", "Salesman");
            Assert.That(result.RoleRequestCount, Is.EqualTo(1));
            result = await roleRequestService.GetRoleRequestsAsync(null, 6, 1, "", "Importer");
            Assert.That(result.RoleRequestCount, Is.EqualTo(1));
        }

        [Test]
        [Order(6)]
        public async Task Test_GetRoleRequestsAsync_WithSorting_ReturnsCorrectRoleRequests()
        {
            var result = await roleRequestService.GetRoleRequestsAsync(null, 1, 12, "", "", RoleRequestSorting.Newest);
            Assert.That(result.RoleRequests.First().Id, Is.EqualTo(2));

            result = await roleRequestService.GetRoleRequestsAsync(null, 1, 12, "", "", RoleRequestSorting.Oldest);
            Assert.That(result.RoleRequests.First().Id, Is.EqualTo(1));
        }

        [Test]
        [Order(7)]
        public async Task Test_GetRoleRequestsByUserIdAsync_ReturnsCorrectRoleRequests()
        {
            var result = await roleRequestService.GetRoleRequestsByUserIdAsync("b5fef437-f504-46d2-926d-3158e54e1932");
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().RoleName, Is.EqualTo("Salesman"));
            result = await roleRequestService.GetRoleRequestsByUserIdAsync("926bee86-8bbd-43f6-bc1c-9639d43531a4");
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().RoleName, Is.EqualTo("Importer"));
        }

        [Test]
        [Order(8)]
        public async Task Test_GetRoleRequestByIdAsync_ReturnsCorrectRoleRequest_IfIdExists()
        {
            var result = await roleRequestService.GetRoleRequestByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.RoleName, Is.EqualTo("Salesman"));
            Assert.That(result.UserModel.UserName, Is.EqualTo("Owner"));
        }

        [Test]
        [Order(9)]
        public async Task Test_GetRoleRequestByIdAsync_ReturnsNull_IfIdDoesNotExist()
        {
            var result = await roleRequestService.GetRoleRequestByIdAsync(0);

            Assert.That(result, Is.Null);
        }

        [Test]
        [Order(10)]
        public async Task Test_DeleteRoleRequestAsync_DeletesRoleRequest()
        {
            var roleRequests = await roleRequestService.GetRoleRequestsAsync();
            Assert.That(roleRequests.RoleRequestCount, Is.EqualTo(2));
            await roleRequestService.DeleteRoleRequestAsync(1);
            roleRequests = await roleRequestService.GetRoleRequestsAsync();
            Assert.That(roleRequests.RoleRequestCount, Is.EqualTo(1));
        }
    }
}
