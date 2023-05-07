using CarSales.Core.Contracts;
using CarSales.Core.Models.RoleRequests;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Admin.Controllers
{
    public class ImportersController : BaseController
    {
        private readonly IImporterService importerService;

        public ImportersController(IImporterService importerService)
        {
            this.importerService = importerService;
        }

        public async Task<IActionResult> GrantImporterRole(string userId, int roleRequestId)
        {
            await importerService.CreateOrRenewImporterAsync(userId);

            var model = new RoleRequestRouteModel()
            {
                UserId = userId,
                RoleName = "Importer",
                RoleRequestId = roleRequestId.ToString()
            };
            return RedirectToAction("GrantRole", "RoleRequests", model);
        }
    }
}
