using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class ReviewsController : BaseController
    {
        private readonly IReviewService reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderReview(int reviewerId, int vehicleId)
        {
            var model = await reviewService.CreateReviewOrderModelAsync(reviewerId, vehicleId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderReview(ReviewOrderModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Details", "Vehicles", new { id = model.VehicleId });
        }
    }
}
