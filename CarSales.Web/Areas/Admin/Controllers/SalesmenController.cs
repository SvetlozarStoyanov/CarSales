using CarSales.Core.Contracts;
using CarSales.Core.Models.RoleRequests;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class SalesmenController : BaseController
    {
        private readonly ISalesmanService salesmanService;

        public SalesmenController(ISalesmanService salesmanService)
        {
            this.salesmanService = salesmanService;
        }

        public async Task<IActionResult> GrantSalesmanRole(string userId, int roleRequestId)
        {
            await salesmanService.CreateOrRenewSalesmanAsync(userId);

            var model = new RoleRequestRouteModel()
            {
                UserId = userId,
                RoleName = "Salesman",
                RoleRequestId = roleRequestId.ToString()
            };
            return RedirectToAction("GrantRole", "RoleRequests", model);
        }


    }
}
