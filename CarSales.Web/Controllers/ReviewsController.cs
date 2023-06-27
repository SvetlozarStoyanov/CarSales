using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Index([FromQuery] ReviewsQueryModel model)
        {
            var queryResult = await reviewService.GetAllReviewsAsync(model.SearchTerm,
                model.VehicleName,
                model.ReviewsPerPage,
                model.CurrentPage,
                model.SelectedReviewTypes,
                model.SelectedVehicleTypes,
                model.ReviewSorting);

            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await reviewService.GetReviewByIdAsync(id);
            return View(model);
        }
    }
}
