using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviews;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Reviewer.Controllers
{
    public class ReviewsController : BaseController
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Index([FromQuery] ReviewsQueryModel model)
        {
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

        public async Task<IActionResult> Mine([FromQuery] ReviewsQueryModel model)
        {
            var queryResult = await reviewService.GetReviewerReviewsAsync(User.Id(),
                model.SearchTerm,
                model.VehicleName,
                model.ReviewsPerPage,
                model.CurrentPage,
                model.SelectedReviewTypes,
                model.SelectedVehicleTypes,
                model.ReviewStatus,
                model.ReviewSorting
                );
            model = queryResult;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await reviewService.GetReviewByIdAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            if (!(await reviewService.CanCreateReviewAsync(User.Id(), id)))
            {
                TempData["error"] = "Cannot create review!";
                return RedirectToAction(nameof(Index));
            }
            var model = await reviewService.CreateReviewCreateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await reviewService.CreateReviewCreateModelAsync(model.Id);
                return View(model);
            }
            await reviewService.CreateCompletedReviewAsync(model);
            TempData["success"] = "Successfully created review!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePreviewModel(ReviewCreateModel model)
        {
            var previewModel = await reviewService.CreateReviewPreviewModelAsync(model);
            return Json(previewModel);
        }
    }
}
