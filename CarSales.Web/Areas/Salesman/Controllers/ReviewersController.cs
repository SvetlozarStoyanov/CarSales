using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviewers;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class ReviewersController : BaseController
    {
        private readonly IReviewerService reviewerService;

        public ReviewersController(IReviewerService reviewerService)
        {
            this.reviewerService = reviewerService;
        }

        public async Task<IActionResult> Index(int vehicleId, [FromQuery] ReviewersQueryModel model)
        {
            var queryResult = await reviewerService.GetReviewersAsync(User.Id(),
                vehicleId,
                model.SearchTerm,
                model.CurrentPage,
                model.ReviewersPerPage,
                model.ReviewerSorting);

            model = queryResult;
            return View(model);
        }
    }
}
