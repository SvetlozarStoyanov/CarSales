using CarSales.Core.Contracts;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Importer.Controllers
{
    public class ImportersController : BaseController
    {
        private readonly IImporterService importerService;
        public ImportersController(IImporterService importerService)
        {
            this.importerService = importerService;
        }

        public async Task<IActionResult> LeaveImporterRole()
        {
            await importerService.RetireImporterAsync(User.Id());
            return RedirectToAction("LogoutAndLogin", "Users", new { area = "" });
        }
    }
}
