using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Reviewer.Controllers
{
    [Area("Reviewer")]
    [Authorize(Roles = "Reviewer")]
    public class BaseController : Controller
    {
       
    }
}
