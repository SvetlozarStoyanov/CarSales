using CarSales.Core.Contracts;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class SalesmenController : BaseController
    {
        private readonly ISalesmanService salesmanService;
        public SalesmenController(ISalesmanService salesmanService)
        {
            this.salesmanService = salesmanService;
        }

        public async Task<IActionResult> LeaveSalesmanRole()
        {
            await salesmanService.RetireSalesmanAsync(User.Id());
            return RedirectToAction("LogoutAndLogin", "Users", new { area = "" });
        }
    }
}
