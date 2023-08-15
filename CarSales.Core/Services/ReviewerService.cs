using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Models.Reviewers;
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

        public async Task<ReviewersQueryModel> GetAllReviewersAsync(string? searchTerm = null)
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
            var queryModel = CreateReviewersQueryModel(searchTerm, reviewers);
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


        public async Task<IDictionary<ReviewType, decimal>> GetReviewTypesAndPricesAsync(int reviewerId)
        {
            var reviewTypesAndPrices = new Dictionary<ReviewType, decimal>();
            var reviewTypes = Enum.GetValues<ReviewType>().ToList();
            var reviewer = await repository.AllReadOnly<Reviewer>()
                .FirstOrDefaultAsync(r => r.Id == reviewerId);
            var prices = new List<decimal>() {
                reviewer.ShortReviewPrice, reviewer.StandartReviewPrice, reviewer.PremiumReviewPrice
            };
            for (int i = 0; i < reviewTypes.Count; i++)
            {
                reviewTypesAndPrices.Add(reviewTypes[i], prices[i]);
            }

            return reviewTypesAndPrices;
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
                ReviewsCount = reviewersCount,
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


        private ReviewersQueryModel CreateReviewersQueryModel(string? searchTerm, List<Reviewer> reviewers)
        {
            var model = new ReviewersQueryModel()
            {
                SearchTerm = searchTerm,
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
