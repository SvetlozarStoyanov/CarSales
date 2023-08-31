using CarSales.Core.Contracts;
using CarSales.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarSales.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IReviewService reviewService;
        public HomeController(ILogger<HomeController> logger, IReviewService reviewService)
        {
            this.logger = logger;
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await reviewService.GetLatestReviewsAsync();
            if (model.Count() > 0)
            {
                ViewBag.HighlightedVehicleReview = await reviewService.GetRandomReviewAsync(model);
                model = model.Where(r => r.Id != ViewBag.HighlightedVehicleReview.Id).ToList();
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}