using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Exceptions;
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


        public async Task<bool> CanCreateReviewAsync(string userId, int reviewId)
        {
            var review = await repository.GetByIdAsync<Review>(reviewId);
            if (review == null || review.ReviewStatus == ReviewStatus.Completed)
            {
                return false;
            }
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .Where(r => r.UserId == userId)
                .Include(r => r.Reviews)
                .FirstOrDefaultAsync();
            if (!reviewer.Reviews.Any(r => r.Id == reviewId))
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<ReviewListModel>> GetLatestReviewsAsync(int? id)
        {
            var reviews = await repository.AllReadOnly<Review>()
                .Where(r => r.ReviewStatus == ReviewStatus.Completed && r.Id != id)
                .OrderByDescending(r => r.Id)
                .Take(6)
                .Select(r => new ReviewListModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Overview = r.Overview.Substring(0, 70).TrimEnd() + "...",
                    ReviewType = r.ReviewType,
                    ReviewerName = $"{r.Reviewer.User.FirstName} {r.Reviewer.User.LastName}",
                    VehicleId = r.VehicleId,
                    VehicleName = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                    VehicleImageUrl = r.Vehicle.ImageUrl,
                    VehicleType = r.Vehicle.VehicleType,
                    VehiclePrice = r.Vehicle.Price,
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<ReviewListModel> GetRandomReviewAsync()
        {
            var latestReviews = await repository.AllReadOnly<Review>()
                .OrderByDescending(v => v.Id)
                .Take(7)
                .Where(r => r.VehicleRating > VehicleRating.Average)
                .Include(r => r.Vehicle)
                .Include(r => r.Reviewer)
                .ThenInclude(rv => rv.User)
                .ToListAsync();

            var random = new Random();
            var randomIndex = random.Next(0, latestReviews.Count);
            var randomReview = latestReviews[randomIndex];

            var review = new ReviewListModel() 
            {
                Id = randomReview.Id,
                Title = randomReview.Title,
                Overview = randomReview.Overview,
                ReviewType = randomReview.ReviewType,
                ReviewerName = $"{randomReview.Reviewer.User.FirstName} {randomReview.Reviewer.User.LastName}",
                VehicleId = randomReview.VehicleId,
                VehicleName = $"{randomReview.Vehicle.Brand} {randomReview.Vehicle.Model}",
                VehicleImageUrl = randomReview.Vehicle.ImageUrl,
                VehicleType = randomReview.Vehicle.VehicleType,
                VehiclePrice = randomReview.Vehicle.Price,
            };

            return review;
        }

        public async Task<ReviewsQueryModel> GetAllReviewsAsync(string? searchTerm = null,
            string? vehicleName = null,
            int reviewsPerPage = 6,
            int currentPage = 1,
            string? selectedReviewTypes = null,
            string? selectedVehicleTypes = null,
            ReviewSorting reviewSorting = ReviewSorting.VehiclePriceDescending)
        {

            var reviews = await repository.AllReadOnly<Review>()
                .Where(r => r.ReviewStatus == ReviewStatus.Completed)
                .Include(r => r.Vehicle)
                .ToListAsync();
            var vehicleNames = reviews.Select(r => $"{r.Vehicle.Brand} {r.Vehicle.Model}").ToHashSet();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                reviews = reviews.Where(r => r.Title.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                reviews = reviews.Where(r => $"{r.Vehicle.Brand} {r.Vehicle.Model}" == vehicleName)
                    .ToList();
            }
            if (!string.IsNullOrWhiteSpace(selectedReviewTypes))
            {
                var selectedReviewTypesSplit = selectedReviewTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                reviews = reviews.Where(r => selectedReviewTypesSplit.Any(srt => srt.ToLower() == r.ReviewType.ToString().ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(selectedVehicleTypes))
            {
                var selectedVehicleTypesSplit = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                reviews = reviews.Where(r => selectedVehicleTypesSplit.Any(svt => svt.ToLower() == r.Vehicle.VehicleType.ToString().ToLower())).ToList();
            }



            switch (reviewSorting)
            {
                case ReviewSorting.VehiclePriceAscending:
                    reviews = reviews.OrderBy(r => r.Vehicle.Price).ToList();
                    break;
                case ReviewSorting.VehiclePriceDescending:
                    reviews = reviews.OrderByDescending(r => r.Vehicle.Price).ToList();
                    break;
                case ReviewSorting.TitleAscending:
                    reviews = reviews.OrderByDescending(r => r.Title).ToList();
                    break;
                case ReviewSorting.TitleDescending:
                    reviews = reviews.OrderBy(r => r.Title).ToList();
                    break;
            }

            var reviewCount = reviews.Count();
            var sortedReviews = reviews.Skip((currentPage - 1) * reviewsPerPage)
                .Take(reviewsPerPage);
            var model = CreateReviewsQueryModel(searchTerm,
                vehicleName,
                reviewCount,
                currentPage,
                reviewsPerPage,
                null,
                reviewSorting,
                selectedReviewTypes,
                selectedVehicleTypes,
                vehicleNames,
                sortedReviews);

            return model;
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
                        VehicleRating = r.VehicleRating
                    }
                })
                .FirstOrDefaultAsync();

            return review;
        }

        public async Task<ReviewsQueryModel> GetReviewerReviewsAsync(string userId,
            string? searchTerm = null,
            string? vehicleName = null,
            int reviewsPerPage = 6,
            int currentPage = 1,
            string? selectedReviewTypes = null,
            string? selectedVehicleTypes = null,
            ReviewStatus? reviewStatus = null,
            ReviewSorting reviewSorting = ReviewSorting.VehiclePriceDescending
            )
        {
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .FirstOrDefaultAsync(r => r.UserId == userId);

            var reviews = await repository.AllReadOnly<Review>()
                .Where(r => r.ReviewerId == reviewer.Id)
                .Include(r => r.Vehicle)
                .ToListAsync();
            var vehicleNames = reviews.Select(r => $"{r.Vehicle.Brand} {r.Vehicle.Model}").ToList();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                reviews = reviews.Where(r => r.Title.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                reviews = reviews.Where(r => $"{r.Vehicle.Brand} {r.Vehicle.Model}" == vehicleName)
                    .ToList();
            }
            if (!string.IsNullOrWhiteSpace(selectedReviewTypes))
            {
                var selectedReviewTypesSplit = selectedReviewTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                reviews = reviews.Where(r => selectedReviewTypesSplit.Any(srt => srt.ToLower() == r.ReviewType.ToString().ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(selectedVehicleTypes))
            {
                var selectedVehicleTypesSplit = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                reviews = reviews.Where(r => selectedVehicleTypesSplit.Any(svt => svt.ToLower() == r.Vehicle.VehicleType.ToString().ToLower())).ToList();
            }

            if (reviewStatus != null)
            {
                reviews = reviews.Where(r => r.ReviewStatus == reviewStatus)
                    .ToList();
            }

            switch (reviewSorting)
            {
                case ReviewSorting.VehiclePriceAscending:
                    reviews = reviews.OrderBy(r => r.Vehicle.Price).ToList();
                    break;
                case ReviewSorting.VehiclePriceDescending:
                    reviews = reviews.OrderByDescending(r => r.Vehicle.Price).ToList();
                    break;
                case ReviewSorting.TitleAscending:
                    reviews = reviews.OrderByDescending(r => r.Title).ToList();
                    break;
                case ReviewSorting.TitleDescending:
                    reviews = reviews.OrderBy(r => r.Title).ToList();
                    break;
            }

            var reviewCount = reviews.Count();
            var sortedReviews = reviews.Skip((currentPage - 1) * reviewsPerPage)
                .Take(reviewsPerPage);
            var model = CreateReviewsQueryModel(searchTerm,
                vehicleName,
                reviewCount,
                currentPage,
                reviewsPerPage,
                reviewStatus,
                reviewSorting,
                selectedReviewTypes,
                selectedVehicleTypes,
                vehicleNames,
                sortedReviews);

            return model;
        }

        public async Task<ReviewCreateModel> CreateReviewCreateModelAsync(int id)
        {
            var model = await repository.AllReadOnly<Review>()
                .Where(r => r.Id == id)
                .Select(r => new ReviewCreateModel()
                {
                    Id = r.Id,
                    VehicleName = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                    ReviewType = r.ReviewType
                })
                .FirstOrDefaultAsync();


            return model;
        }

        public async Task<ReviewOrderModel> CreateReviewOrderModel(int reviewerId, int vehicleId, IDictionary<ReviewType, decimal> reviewTypesAndPrices)
        {
            var model = new ReviewOrderModel()
            {
                ReviewerId = reviewerId,
                VehicleId = vehicleId,
                ReviewTypesAndPrices = reviewTypesAndPrices
            };
            return model;
        }

        public async Task CreateOrderedReviewAsync(string userId, ReviewOrderModel model)
        {
            var salesman = await repository.All<Salesman>()
                .Where(s => s.UserId == userId)
                .Include(s => s.User)
                .FirstOrDefaultAsync();

            var reviewer = await repository.All<Reviewer>()
                .Where(r => r.Id == model.ReviewerId)
                .Include(s => s.User)
                .FirstOrDefaultAsync();

            var reviewTypeEnumLength = Enum.GetValues<ReviewType>().Length;
            if (model.ReviewTypeIndex < 0 || model.ReviewTypeIndex >= reviewTypeEnumLength)
            {
                throw new ArgumentOutOfRangeException("Out of bounds of Enum");
            }
            model.ReviewType = (ReviewType)model.ReviewTypeIndex;
            model.Price = model.ReviewTypesAndPrices[model.ReviewType];
            if (salesman.User.Credits < model.Price)
            {
                throw new InsufficientCreditsException("You do not have enough credits for this purchase!");
            }

            var review = new Review()
            {
                Title = "",
                Overview = "",
                Performance = "",
                ReviewerId = model.ReviewerId,
                VehicleId = model.VehicleId,
                ReviewType = model.ReviewType,
                ReviewStatus = ReviewStatus.Ordered,
                Price = model.Price,
            };

            salesman.User.Credits -= model.Price;
            reviewer.User.Credits += model.Price;

            await repository.AddAsync<Review>(review);

            await repository.SaveChangesAsync();
        }
        public async Task CreateCompletedReviewAsync(ReviewCreateModel model)
        {
            var review = await repository.All<Review>()
                .FirstOrDefaultAsync(r => r.Id == model.Id);

            review.Title = model.Title;
            review.Overview = model.Overview;
            review.Performance = model.Performance;
            review.Interior = model.Interior;
            review.Longevity = model.Longevity;
            review.Features = model.Features;
            review.ReviewStatus = ReviewStatus.Completed;
            review.VehicleRating = model.VehicleRating;

            await repository.SaveChangesAsync();
        }

        private ReviewsQueryModel CreateReviewsQueryModel(string? searchTerm,
            string? vehicleName,
            int reviewCount,
            int currentPage,
            int reviewsPerPage,
            ReviewStatus? reviewStatus,
            ReviewSorting reviewSorting,
            string selectedReviewTypes,
            string selectedVehicleTypes,
            ICollection<string> vehicleNames,
            IEnumerable<Review> reviews)
        {

            var model = new ReviewsQueryModel()
            {
                SearchTerm = searchTerm,
                VehicleName = vehicleName,
                CurrentPage = currentPage,
                MaxPage = (int)Math.Ceiling(((double)reviewCount / reviewsPerPage)),
                ReviewsPerPage = reviewsPerPage,
                ReviewCount = reviewCount,
                ReviewStatus = reviewStatus,
                ReviewSorting = reviewSorting,
                SelectedReviewTypes = selectedReviewTypes,
                SelectedVehicleTypes = selectedVehicleTypes,
                VehicleNames = vehicleNames,
                Reviews = reviews.Select(r => new ReviewListModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Overview = r.Overview,
                    Price = r.Price,
                    ReviewType = r.ReviewType,
                    ReviewStatus = r.ReviewStatus,
                    ReviewerId = r.ReviewerId,
                    VehicleId = r.VehicleId,
                    VehicleName = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                    VehicleImageUrl = r.Vehicle.ImageUrl,
                    VehicleType = r.Vehicle.VehicleType,
                    VehiclePrice = r.Vehicle.Price,
                })
            };
            if (model.MaxPage > 1)
            {
                var previousPages = new HashSet<int>();
                var nextPages = new HashSet<int>();
                //var pagesToMaxPage = model.MaxPage - model.CurrentPage;
                var numberOfPages = 0;
                var index = 1;
                while (numberOfPages < 4 && numberOfPages < model.MaxPage - 1)
                {
                    var previousPage = model.CurrentPage - index;
                    var nextPage = model.CurrentPage + index;
                    if (previousPage >= 1)
                    {
                        previousPages.Add(previousPage);
                        numberOfPages++;
                    }
                    if (nextPage <= model.MaxPage)
                    {
                        nextPages.Add(nextPage);
                        numberOfPages++;
                    }
                    index++;
                }
                model.PreviousPages = previousPages.Reverse();
                model.NextPages = nextPages;
            }

            return model;
        }


    }
}
