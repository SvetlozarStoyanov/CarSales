using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviews;
using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;
        public ReviewService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ReviewListModel>> GetLatestReviewsAsync()
        {
            var reviews = await repository.AllReadOnly<Review>()
                .Where(r => r.ReviewStatus == ReviewStatus.Completed)
                .OrderByDescending(r => r.Id)
                .Take(6)
                .Select(r => new ReviewListModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    ReviewerName = $"{r.Reviewer.User.FirstName} {r.Reviewer.User.LastName}",
                    VehicleName = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                    VehiclePrice = r.Vehicle.Price,
                    ReviewType = r.ReviewType
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<ReviewViewModel> GetReviewByIdAsync(int id)
        {
            var review = await repository.AllReadOnly<Review>()
                .Where(r => r.Id == id)
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Overview = r.Overview,
                    Performance = r.Performance,
                    Interior = r.Interior,
                    Longevity = r.Longevity,
                    Features = r.Features,
                    ReviewerId = r.ReviewerId,
                    ReviewerName = $"{r.Reviewer.User.FirstName} {r.Reviewer.User.LastName}",
                    Vehicle = new VehicleReviewModel
                    {
                        Id = r.VehicleId,
                        Name = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                        Price = r.Vehicle.Price,
                        ImageUrl = r.Vehicle.ImageUrl,
                        VehicleType = r.Vehicle.VehicleType,
                        VehicleRating = r.Vehicle.VehicleRating
                    }
                })
                .FirstOrDefaultAsync();

            return review;
        }



        public async Task<ReviewCreateModel> CreateReviewCreateModelAsync(int reviewerId, int vehicleId, decimal price, ReviewType reviewType)
        {
            var model = new ReviewCreateModel()
            {
                ReviewerId = reviewerId,
                VehicleId = vehicleId,
                Price = price,
                ReviewType = reviewType
            };

            return model;
        }

        public async Task<ReviewOrderModel> CreateReviewOrderModelAsync(int reviewerId, int vehicleId)
        {
            var reviewTypes = Enum.GetValues<ReviewType>().ToList();
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .FirstOrDefaultAsync(r => r.Id == reviewerId);
            var prices = new List<decimal>() {
                reviewer.ShortReviewPrice, reviewer.StandartReviewPrice, reviewer.PremiumReviewPrice
            };
          
            var model = new ReviewOrderModel()
            {
                ReviewerId = reviewerId,
                VehicleId = vehicleId,
            };
            for (int i = 0; i < reviewTypes.Count; i++)
            {
                model.ReviewTypesAndPrices.Add(reviewTypes[i], prices[i]);
            }
            return model;
        }

        public async Task CreateOrderedReviewAsync(ReviewOrderModel model)
        {
            var review = new Review()
            {
                ReviewerId = model.ReviewerId,
                VehicleId = model.VehicleId,
                ReviewType = model.ReviewType,
                ReviewStatus = ReviewStatus.Ordered,
                Price = model.Price,
            };

            await repository.AddAsync<Review>(review);

            await repository.SaveChangesAsync();
        }
        public async Task CreateCompletedReviewAsync(ReviewCreateModel model)
        {
            var review = new Review()
            {
                Title = model.Title,
                Overview = model.Overview,
                Performance = model.Performance,
                Interior = model.Interior,
                Longevity = model.Longevity,
                Features = model.Features,
                ReviewerId = model.ReviewerId,
                VehicleId = model.VehicleId,
                ReviewType = model.ReviewType,
                ReviewStatus = ReviewStatus.Completed,
                Price = model.Price,
            };

            await repository.AddAsync<Review>(review);

            await repository.SaveChangesAsync();
        }


    }
}
