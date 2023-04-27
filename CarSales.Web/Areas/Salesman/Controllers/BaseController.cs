using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    [Area("Salesman")]
    [Authorize(Roles = "Salesman")]
    public class BaseController : Controller
    {

    }
}
