using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Importer.Controllers
{
    public class ReviewsController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public ReviewsController(IReviewService reviewService, IHtmlSanitizingService htmlSanitizingService)
        {
            this.reviewService = reviewService;
            this.htmlSanitizingService = htmlSanitizingService;
        }

        public async Task<IActionResult> Index([FromQuery] ReviewsQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await reviewService.GetReviewsAsync(model.SearchTerm,
                model.VehicleName,
                model.ReviewsPerPage,
                model.CurrentPage,
                model.SelectedReviewTypes,
                model.SelectedVehicleTypes,
                model.ReviewSorting
                );
            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await reviewService.GetReviewByIdAsync(id);
            if (model == null)
            {
                TempData["error"] = "Review does not exist!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
