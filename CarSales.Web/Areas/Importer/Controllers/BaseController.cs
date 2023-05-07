using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Importer.Controllers
{
    [Area("Importer")]
    [Authorize(Roles = "Importer")]
    public class BaseController : Controller
    {

    }
}
