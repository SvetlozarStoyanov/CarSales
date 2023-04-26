using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Owner.Controllers
{
    [Area("Owner")]
    //[Route("/Owner/[controller]/[Action]/{id?}")]
    [Authorize(Roles = "Owner")]
    public class BaseController : Controller
    {
       
    }
}
