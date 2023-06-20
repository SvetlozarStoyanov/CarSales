using CarSales.Core.Contracts;
using CarSales.Core.Models.Reviewers;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Reviewer.Controllers
{
    public class ReviewersController : BaseController
    {
        private readonly IReviewerService reviewerService;

        public ReviewersController(IReviewerService reviewerService)
        {
            this.reviewerService = reviewerService;
        }

        [HttpGet]
        public async Task<IActionResult> EditReviewPrices()
        {
            var model = await reviewerService.CreateReviewerPriceEditModelAsync(User.Id());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditReviewPrices(ReviewerPriceEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await reviewerService.EditReviewPricesAsync(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
