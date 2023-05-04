using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await vehicleService.GetAllVehiclesWhichAreForSaleAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await vehicleService.GetVehicleByIdAsync(id);
            if (model == null)
            {
                TempData["error"] = "Vehicle does not exist!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Mine()
        {
            var model = await vehicleService.GetOwnerVehiclesAsync(User.Id());
            return View(model);
        }

        public async Task<IActionResult> MyVehiclesOnSale()
        {
            var model = await vehicleService.GetSalesmanVehiclesAsync(User.Id());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRating(VehicleChangeRatingModel model)
        {
            await vehicleService.ChangeVehicleRatingAsync(model.Id, (int)model.NewRating);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        public async Task<IActionResult> Buy(int id)
        {
            await vehicleService.BuyVehicleAsync(id, User.Id());
            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpGet]
        public async Task<IActionResult> Sell(int id)
        {
            var model = await vehicleService.CreateVehicleSellModel(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Sell(VehicleSellModel model)
        {
            try
            {
                if (model.OldPrice > model.Price)
                {
                    model = await vehicleService.CreateVehicleSellModel(model.Id);
                    return View(model);
                }
                await vehicleService.PutVehicleForSaleAsync(model);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction(nameof(MyVehiclesOnSale));
        }
    }
}
