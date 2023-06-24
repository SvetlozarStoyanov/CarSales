using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Reviewer.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly IOfferService offerService;
        private readonly IUserService userService;

        public VehiclesController(IVehicleService vehicleService, IOfferService offerService, IUserService userService)
        {
            this.vehicleService = vehicleService;
            this.offerService = offerService;
            this.userService = userService;
        }
        public async Task<IActionResult> Index([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetAllVehiclesForSaleAsync(
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model.SearchTerm = queryResult.SearchTerm;
            model.VehiclesPerPage = queryResult.VehiclesPerPage;
            model.CurrentPage = queryResult.CurrentPage;
            model.VehiclesCount = queryResult.VehiclesCount;
            model.VehicleSorting = queryResult.VehicleSorting;
            model.SortingOptions = queryResult.SortingOptions;
            model.VehicleTypes = queryResult.VehicleTypes;
            model.SelectedVehicleTypes = queryResult.SelectedVehicleTypes;
            model.Vehicles = queryResult.Vehicles;

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
            ViewBag.CanMakeOffer = await offerService.CanCreateOfferAsync(User.Id(), id);
            if (!ViewBag.CanMakeOffer)
            {
                var offerId = await offerService.GetOfferIdAsync(User.Id(), id);
                ViewBag.AvailableCredits = await userService.GetUserAvailableCreditsAsync(User.Id(), offerId);
            }
            else
            {
                ViewBag.AvailableCredits = await userService.GetUserAvailableCreditsAsync(User.Id());
            }
            return View(model);
        }

        public async Task<IActionResult> Mine([FromQuery] VehiclesQueryModel model)
        {
            var queryResult = await vehicleService.GetOwnerVehiclesAsync(
                User.Id(),
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model.SearchTerm = queryResult.SearchTerm;
            model.VehiclesPerPage = queryResult.VehiclesPerPage;
            model.CurrentPage = queryResult.CurrentPage;
            model.VehiclesCount = queryResult.VehiclesCount;
            model.VehicleSorting = queryResult.VehicleSorting;
            model.SortingOptions = queryResult.SortingOptions;
            model.VehicleTypes = queryResult.VehicleTypes;
            model.SelectedVehicleTypes = queryResult.SelectedVehicleTypes;
            model.Vehicles = queryResult.Vehicles;

            return View(model);
        }


        
        public async Task<IActionResult> Buy(int id)
        {
            try
            {
                await vehicleService.BuyVehicleFromSalesmanAsync(id, User.Id());
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
