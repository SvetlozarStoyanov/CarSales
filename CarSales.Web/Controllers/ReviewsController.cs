using CarSales.Core.Contracts;
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

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await reviewService.GetReviewByIdAsync(id);
            return View(model);
        }
    }
}
