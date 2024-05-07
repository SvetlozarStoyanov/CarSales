using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Services;
using CarSales.Infrastructure.Data;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CarSales.Tests.UnitTests
{
    public class ReviewerServiceTests
    {
        private const string SALESMAN_ID = "66ccb670-f0dd-4aa1-a83d-8b2a0003bb50";
        private int vehicleForSaleId = 0;
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

            var vehicleForSale = new Vehicle()
            {
                Brand = "Test",
                Model = "Test",
                Description = "Test Vehicle",
                SalesmanId = 1,
                TopSpeed = 260,
                KilometersDriven = 0,
                ImageUrl = null,
                VehicleType = VehicleType.Car,
                Price = 10000,
                YearProduced = DateTime.Now.Year
            };

            await repository.AddAsync<Vehicle>(vehicleForSale);
            await repository.SaveChangesAsync();

            vehicleForSaleId = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Brand == "Test")
                .Select(v => v.Id)
                .FirstOrDefaultAsync();
        }

        [Test]
        public async Task Test_CanBeOrderedToCreateReviewAsync_ReturnsTrue_IfReviewerCanCreateReview()
        {
            var result = await reviewerService.CanBeOrderedToCreateReviewAsync(1, vehicleForSaleId);
            Assert.That(result,Is.True);
        }

        [Test]
        public async Task Test_GetReviewTypesAndPricesAsync_ReturnsCorrectReviewTypesAndPrices_IfIdExists()
        {
            var result = await reviewerService.GetReviewTypesAndPricesAsync(1);
            var reviewer = await repository.GetByIdAsync<Reviewer>(1);
            Assert.That(result[ReviewType.Short], Is.EqualTo(reviewer.ShortReviewPrice));
            Assert.That(result[ReviewType.Standart], Is.EqualTo(reviewer.StandartReviewPrice));
            Assert.That(result[ReviewType.Premium], Is.EqualTo(reviewer.PremiumReviewPrice));

            result = await reviewerService.GetReviewTypesAndPricesAsync(2);
            reviewer = await repository.GetByIdAsync<Reviewer>(2);
            Assert.That(result[ReviewType.Short], Is.EqualTo(reviewer.ShortReviewPrice));
            Assert.That(result[ReviewType.Standart], Is.EqualTo(reviewer.StandartReviewPrice));
            Assert.That(result[ReviewType.Premium], Is.EqualTo(reviewer.PremiumReviewPrice));
        }

        [Test]
        public async Task Test_GetReviewTypesAndPricesAsync_ReturnsCorrectReviewTypesAndPrices_IfIdDoesNotExist()
        {
            var result = await reviewerService.GetReviewTypesAndPricesAsync(0);
            Assert.That(result, Is.Null);
        }


        [Test]
        public async Task Test_CanBeOrderedToCreateReviewAsync_ReturnsFalse_IfReviewerHasAlreadyCreatedOrBeenOrderedToCreateReview()
        {
            var result = await reviewerService.CanBeOrderedToCreateReviewAsync(1, 1);
            Assert.That(result, Is.False);
        }


        [Test]
        public async Task Test_GetReviewersAsync_ForAdmin_WithNoFilters_ReturnsCorrectReviewers()
        {
            var result = await reviewerService.GetReviewersAsync();
            Assert.That(result.ReviewerCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetReviewersAsync_ForAdmin_WithSearchTerm_ReturnsCorrectReviewers()
        {
            var result = await reviewerService.GetReviewersAsync("Reviewer2");
            Assert.That(result.ReviewerCount, Is.EqualTo(1));
            result = await reviewerService.GetReviewersAsync("Reviewer");
            Assert.That(result.ReviewerCount, Is.EqualTo(2));
            result = await reviewerService.GetReviewersAsync("Not Found");
            Assert.That(result.ReviewerCount, Is.EqualTo(0));
        }


        [Test]
        public async Task Test_GetReviewersAsync_ForSalesman_WithNoFilters_ReturnsCorrectReviewers()
        {
            var result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId);
            Assert.That(result.ReviewerCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Test_GetReviewersAsync_ForSalesman_WithSearchTerm_ReturnsCorrectReviewers()
        {
            var result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "Reviewer");
            Assert.That(result.ReviewerCount, Is.EqualTo(2));
            result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "Reviewer2");
            Assert.That(result.ReviewerCount, Is.EqualTo(1));
            result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "Not found!");
            Assert.That(result.ReviewerCount, Is.EqualTo(0));
        }

        [Test]
        public async Task Test_GetReviewersAsync_ForSalesman_WithReviewersPerPage_ReturnsCorrectReviewers()
        {
            var result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "", 1, 2);
            Assert.That(result.ReviewerCount, Is.EqualTo(2));
            result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "", 1, 1);
            Assert.That(result.Reviewers.Count, Is.EqualTo(1));
            result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "", 2, 1);
            Assert.That(result.Reviewers.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Test_GetReviewersAsync_ForSalesman_WithSorting_ReturnsCorrectReviewers()
        {
            var result = await reviewerService.GetReviewersAsync(SALESMAN_ID, vehicleForSaleId, "", 1, 6, ReviewerSorting.NumberOfReviews);
            Assert.That(result.Reviewers.First().Name, Is.EqualTo("Reviewer Test"));
        }

        [Test]
        public async Task Test_GetReviewerByIdAsync_ReturnsCorrectReviewer_IfIdExists()
        {
            var result = await reviewerService.GetReviewerByIdAsync(1, 1);
            Assert.That(result.CanCreateReview, Is.EqualTo(false));
            Assert.That(result.Name, Is.EqualTo("Reviewer Test"));
        }

        [Test]
        public async Task Test_GetReviewerByIdAsync_ReturnsNull_IfIdDoesNotExist()
        {
            var result = await reviewerService.GetReviewerByIdAsync(0, 1);
            Assert.That(result, Is.EqualTo(null));
        }
    }
}
