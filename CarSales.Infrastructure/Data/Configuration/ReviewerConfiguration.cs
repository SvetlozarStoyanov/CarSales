using CarSales.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSales.Infrastructure.Data.Configuration
{
    public class ReviewerConfiguration : IEntityTypeConfiguration<Reviewer>
    {
        public void Configure(EntityTypeBuilder<Reviewer> builder)
        {
            builder.HasData(CreateReviewers());
        }

        private List<Reviewer> CreateReviewers()
        {
            var reviewers = new List<Reviewer>()
            {
                new Reviewer()
                {
                    Id = 1,
                    UserId = "4d693871-c20b-4e9f-8490-1c641b9e3a40",
                    ShortReviewPrice = 100,
                    StandartReviewPrice = 150,
                    PremiumReviewPrice = 200,
                },
                new Reviewer()
                {
                    Id = 2,
                    UserId = "9b92fe41-3f2e-4eb1-990b-73c2ea2d746d",
                    ShortReviewPrice = 100,
                    StandartReviewPrice = 200,
                    PremiumReviewPrice = 250,
                }
            };
            return reviewers;
        }

    }
}
