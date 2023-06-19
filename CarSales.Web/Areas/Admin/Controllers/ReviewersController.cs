using CarSales.Core.Contracts;
using CarSales.Core.Models.RoleRequests;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class ReviewersController : BaseController
    {
        private readonly IReviewerService reviewerService;

        public ReviewersController(IReviewerService salesmanService)
        {
            this.reviewerService = salesmanService;
        }

        public async Task<IActionResult> GrantReviewerRole(string userId, int roleRequestId)
        {
            await reviewerService.CreateOrRenewReviewerAsync(userId);

            var model = new RoleRequestRouteModel()
            {
                UserId = userId,
                RoleName = "Reviewer",
                RoleRequestId = roleRequestId.ToString()
            };
            return RedirectToAction("GrantRole", "RoleRequests", model);
        }


    }
}
