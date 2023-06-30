using CarSales.Core.Contracts;
using CarSales.Core.Exceptions;
using CarSales.Core.Models.Reviews;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class ReviewsController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IReviewerService reviewerService;
        public ReviewsController(IReviewService reviewService, IReviewerService reviewerService)
        {
            this.reviewService = reviewService;
            this.reviewerService = reviewerService;
        }

        public async Task<IActionResult> Index([FromQuery] ReviewsQueryModel model)
        {
            var queryResult = await reviewService.GetAllReviewsAsync(model.SearchTerm,
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
            return View(model);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(int reviewerId, int vehicleId)
        {
            var reviewTypesAndPrices = await reviewerService.GetReviewTypesAndPricesAsync(reviewerId);
            var model = await reviewService.CreateReviewOrderModel(reviewerId, vehicleId, reviewTypesAndPrices);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(ReviewOrderModel model)
        {
            var reviewTypesAndPrices = await reviewerService.GetReviewTypesAndPricesAsync(model.ReviewerId);
            if (!ModelState.IsValid)
            {
                model = await reviewService.CreateReviewOrderModel(model.ReviewerId, model.VehicleId, reviewTypesAndPrices);
                return View(model);
            }
            model.ReviewTypesAndPrices = reviewTypesAndPrices;

            try
            {
                await reviewService.CreateOrderedReviewAsync(User.Id(), model);
            }
            catch (InsufficientCreditsException e)
            {
                TempData["error"] = e.Message;
                model = await reviewService.CreateReviewOrderModel(model.ReviewerId, model.VehicleId, reviewTypesAndPrices);
                return View(model);
            }
            catch (Exception e)
            {
                model = await reviewService.CreateReviewOrderModel(model.ReviewerId, model.VehicleId, reviewTypesAndPrices);
                return View(model);
            }

            return RedirectToAction("Details", "Vehicles", new { id = model.VehicleId });
        }
    }
}
