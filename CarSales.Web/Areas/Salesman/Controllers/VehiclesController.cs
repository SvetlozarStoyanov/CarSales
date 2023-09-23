using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Salesman.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly IOfferService offerService;
        private readonly IUserService userService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public VehiclesController(IVehicleService vehicleService,
            IOfferService offerService,
            IUserService userService,
            IHtmlSanitizingService htmlSanitizingService)
        {
            this.vehicleService = vehicleService;
            this.offerService = offerService;
            this.userService = userService;
            this.htmlSanitizingService = htmlSanitizingService;
        }

        public async Task<IActionResult> Index([FromQuery] VehiclesQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await vehicleService.GetVehiclesForSaleAsync(
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

            return View(model);
        }

        public async Task<IActionResult> Imported([FromQuery] VehiclesQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await vehicleService.GetImportedVehiclesAsync(
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

            return View(model);
        }

        public async Task<IActionResult> Mine([FromQuery] VehiclesQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model); 
            var queryResult = await vehicleService.GetOwnerVehiclesAsync(
                User.Id(),
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

            return View(model);
        }

        public async Task<IActionResult> MyVehiclesOnSale([FromQuery] VehiclesQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await vehicleService.GetSalesmanVehiclesAsync(
                User.Id(),
                model.SearchTerm,
                model.VehiclesPerPage,
                model.CurrentPage,
                model.SelectedVehicleTypes,
                model.VehicleSorting);

            model = queryResult;

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
            ViewBag.CanCreateOffer = await offerService.CanCreateOfferAsync(User.Id(), id);
            if (!ViewBag.CanCreateOffer)
            {
                var offerId = await offerService.GetOfferIdAsync(User.Id(), id);
                if (await offerService.CanEditOfferAsync(User.Id(), id))
                {
                    model.OfferEditModel = await offerService.CreateOfferEditModelAsync(offerId);
                }
                ViewBag.AvailableCredits = await userService.GetUserAvailableCreditsAsync(User.Id(), offerId);
            }
            else
            {
                model.OfferCreateModel = await offerService.CreateOfferCreateModelAsync(User.Id(), id);
                ViewBag.AvailableCredits = await userService.GetUserAvailableCreditsAsync(User.Id());
            }
            return View(model);
        }

        public async Task<IActionResult> BuyFromSalesman(int id)
        {
            try
            {
                await vehicleService.BuyVehicleFromSalesmanAsync(id, User.Id());
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> BuyFromImporter(int id)
        {
            try
            {
                await vehicleService.BuyVehicleFromImporterAsync(id, User.Id());
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Imported));
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> Sell(VehicleSellModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            try
            {
                if (model.OldPrice < model.Price)
                {
                    model = await vehicleService.CreateVehicleSellModelAsync(model.Id);
                    return View(model);
                }
                await vehicleService.PutVehicleForSaleAsync(model);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Edit(VehicleEditModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            try
            {
                if (model.OldPrice < model.Price)
                {
                    return View(model);
                }
                await vehicleService.EditVehicleAsync(model);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }
}
