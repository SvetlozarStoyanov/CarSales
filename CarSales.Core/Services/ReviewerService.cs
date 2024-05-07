using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Models.Reviewers;
using CarSales.Core.Models.Reviews;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class ReviewerService : IReviewerService
    {
        private readonly IRepository repository;
        public ReviewerService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> CanBeOrderedToCreateReviewAsync(int reviewerId, int vehicleId)
        {
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .Where(r => r.Id == reviewerId && !r.Reviews.Any(rv => rv.VehicleId == vehicleId))
                .FirstOrDefaultAsync();
            if (reviewer == null)
            {
                return false;
            }
            return true;
        }

        public async Task CreateOrRenewReviewerAsync(string userId)
        {
            var reviewer = await repository.All<Reviewer>()
                .FirstOrDefaultAsync(s => s.UserId == userId);
            if (reviewer != null)
            {
                reviewer.IsActive = true;
            }
            else
            {
                reviewer = new Reviewer()
                {
                    UserId = userId,
                    ShortReviewPrice = 100,
                    StandartReviewPrice = 150,
                    PremiumReviewPrice = 200,
                    //SalesmanRating = SalesmanRating.Average
                };
                await repository.AddAsync<Reviewer>(reviewer);
            }
            await repository.SaveChangesAsync();
        }

        public async Task RetireReviewerAsync(string userId)
        {
            var reviewer = await repository.All<Reviewer>()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (reviewer != null)
            {
                reviewer.IsActive = false;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<string> GetReviewerUserId(int id)
        {
            var reviewer = await repository.GetByIdAsync<Reviewer>(id);
            return reviewer.UserId;
        }

        public async Task<IDictionary<ReviewType, decimal>> GetReviewTypesAndPricesAsync(int reviewerId)
        {
            var reviewTypesAndPrices = new Dictionary<ReviewType, decimal>();
            var reviewTypes = Enum.GetValues<ReviewType>().ToList();
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .FirstOrDefaultAsync(r => r.Id == reviewerId);
            if (reviewer == null)
            {
                return null;
            }
            var prices = new List<decimal>() {
                reviewer.ShortReviewPrice, reviewer.StandartReviewPrice, reviewer.PremiumReviewPrice
            };
            for (int i = 0; i < reviewTypes.Count; i++)
            {
                reviewTypesAndPrices.Add(reviewTypes[i], prices[i]);
            }

            return reviewTypesAndPrices;
        }

        public async Task<ReviewersQueryModel> GetReviewersAsync(string? searchTerm = null)
        {
            var reviewers = await repository.AllReadOnly<Reviewer>()
                .Where(r => r.IsActive)
                .Include(r => r.User)
                .ToListAsync();


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                reviewers = reviewers.Where(r => r.User.UserName.ToLower().Contains(searchTerm.ToLower()) || $"{r.User.FirstName} {r.User.LastName}".Contains(searchTerm.ToLower()))
                    .ToList();
            }
            var reviewerCount = reviewers.Count();
            var queryModel = CreateReviewersQueryModel(searchTerm, reviewerCount, reviewers);
            return queryModel;
        }

        public async Task<ReviewersQueryModel> GetReviewersAsync(string userId,
            int vehicleId,
            string? searchTerm = null,
            int currentPage = 1,
            int reviewersPerPage = 6,
            ReviewerSorting sorting = ReviewerSorting.Alphabetically)
        {
            var reviewers = await repository.AllReadOnly<Reviewer>()
                .Where(r => r.IsActive && r.UserId != userId && !r.Reviews.Any(r => r.VehicleId == vehicleId))
                .Include(r => r.User)
                .Include(r => r.Reviews)
                .ToListAsync();



            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                reviewers = reviewers.Where(r => r.User.UserName.ToLower().Contains(searchTerm.ToLower())
                || $"{r.User.FirstName} {r.User.LastName}".ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
            switch (sorting)
            {
                case ReviewerSorting.Alphabetically:
                    reviewers = reviewers.OrderBy(r => $"{r.User.FirstName} {r.User.LastName}").ToList();
                    break;
                case ReviewerSorting.NumberOfReviews:
                    reviewers = reviewers.OrderByDescending(r => r.Reviews.Count).ToList();
                    break;
                case ReviewerSorting.Rating:
                    break;
            }
            var reviewersCount = reviewers.Count();
            var sortedReviewers = reviewers.Skip((currentPage - 1) * reviewersPerPage)
                .Take(reviewersPerPage)
                .ToList();
            var queryModel = CreateReviewersQueryModel(searchTerm, currentPage, reviewersPerPage, sorting, reviewersCount, sortedReviewers);
            return queryModel;
        }

        public async Task<ReviewerViewModel> GetReviewerByIdAsync(int reviewerId, int vehicleId)
        {
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .Where(r => r.Id == reviewerId)
                .Select(r => new ReviewerViewModel()
                {
                    Id = r.Id,
                    Name = $"{r.User.FirstName} {r.User.LastName}",
                    Email = r.User.Email,
                    CanCreateReview = r.Reviews.All(r => r.VehicleId != vehicleId),
                    ProfilePicture = r.User.ImageUrl != null ? r.User.ImageUrl : $"/img/Profile/{r.User.Gender}Profile.png",
                    PhoneNumber = r.User.PhoneNumber,
                    VehicleId = vehicleId,
                    ShortReviewPrice = r.ShortReviewPrice,
                    StandartReviewPrice = r.StandartReviewPrice,
                    PremiumReviewPrice = r.PremiumReviewPrice,
                    Reviews = r.Reviews
                    .OrderByDescending(r => r.Id)
                    .Take(3)
                    .Select(rv => new ReviewListModel()
                    {
                        Id = rv.Id,
                        Title = rv.Title,
                        Overview = rv.Overview,
                        Price = rv.Price,
                        ReviewType = rv.ReviewType,
                        ReviewStatus = rv.ReviewStatus,
                        ReviewerId = rv.ReviewerId,
                        VehicleId = rv.VehicleId,
                        VehicleName = $"{rv.Vehicle.Brand} {rv.Vehicle.Model}",
                        VehicleImageUrl = rv.Vehicle.ImageUrl,
                        VehicleType = rv.Vehicle.VehicleType,
                        VehiclePrice = rv.Vehicle.Price,
                    })
                })
                .FirstOrDefaultAsync();

            return reviewer;
        }

        public async Task<ReviewerPriceEditModel> CreateReviewerPriceEditModelAsync(string userId)
        {
            var model = await repository.All<Reviewer>()
                .Where(r => r.UserId == userId)
                .Select(r => new ReviewerPriceEditModel()
                {
                    Id = r.Id,
                    ShortReviewPrice = r.ShortReviewPrice,
                    StandartReviewPrice = r.StandartReviewPrice,
                    PremiumReviewPrice = r.PremiumReviewPrice
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task EditReviewPricesAsync(ReviewerPriceEditModel model)
        {
            var reviewer = await repository.All<Reviewer>()
                .FirstOrDefaultAsync(r => r.Id == model.Id);

            reviewer.ShortReviewPrice = model.ShortReviewPrice;
            reviewer.StandartReviewPrice = model.StandartReviewPrice;
            reviewer.PremiumReviewPrice = model.PremiumReviewPrice;

            await repository.SaveChangesAsync();
        }

        private ReviewersQueryModel CreateReviewersQueryModel(string? searchTerm,
            int currentPage,
            int reviewersPerPage,
            ReviewerSorting sorting,
            int reviewersCount,
            List<Reviewer> reviewers)
        {
            var model = new ReviewersQueryModel()
            {
                SearchTerm = searchTerm,
                CurrentPage = currentPage,
                MaxPage = (int)(reviewersCount / reviewersPerPage),
                ReviewersPerPage = reviewersPerPage,
                ReviewerSorting = sorting,
                ReviewerCount = reviewersCount,
                Reviewers = reviewers
                .Select(r => new ReviewerListModel()
                {
                    Id = r.Id,
                    Name = $"{r.User.FirstName} {r.User.LastName}",
                    ProfilePicture = r.User.ImageUrl != null ? r.User.ImageUrl : $"/img/Profile/{r.User.Gender}Profile.png",
                    ShortReviewPrice = r.ShortReviewPrice,
                    StandartReviewPrice = r.StandartReviewPrice,
                    PremiumReviewPrice = r.PremiumReviewPrice,

                }).ToList()
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


        private ReviewersQueryModel CreateReviewersQueryModel(string? searchTerm, int reviewerCount, List<Reviewer> reviewers)
        {
            var model = new ReviewersQueryModel()
            {
                SearchTerm = searchTerm,
                ReviewerCount = reviewerCount,
                Reviewers = reviewers
                .Select(r => new ReviewerListModel()
                {
                    Id = r.Id,
                    Name = $"{r.User.FirstName} {r.User.LastName}",
                    ShortReviewPrice = r.ShortReviewPrice,
                    StandartReviewPrice = r.StandartReviewPrice,
                    PremiumReviewPrice = r.PremiumReviewPrice,
                }).ToList()
            };

            return model;
        }
    }
}
