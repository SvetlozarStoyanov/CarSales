using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviewers;
using CarSales.Core.Services;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class ReviewersController : BaseController
    {
        private readonly IReviewerService reviewerService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public ReviewersController(IReviewerService reviewerService, IHtmlSanitizingService htmlSanitizingService)
        {
            this.reviewerService = reviewerService;
            this.htmlSanitizingService = htmlSanitizingService;
        }

        public async Task<IActionResult> Index(int vehicleId, [FromQuery] ReviewersQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await reviewerService.GetReviewersAsync(User.Id(),
                vehicleId,
                model.SearchTerm,
                model.CurrentPage,
                model.ReviewersPerPage,
                model.ReviewerSorting);

            ViewBag.VehicleId = vehicleId;
            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Details(int reviewerId, int vehicleId)
        {
            var model = await reviewerService.GetReviewerByIdAsync(reviewerId, vehicleId);
            return View(model);
        }
    }
}
