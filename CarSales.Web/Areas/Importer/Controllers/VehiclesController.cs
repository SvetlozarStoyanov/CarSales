using CarSales.Core.Contracts;
using CarSales.Core.Models.Vehicles;
using CarSales.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Web.Areas.Importer.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly IOfferService offerService;
        private readonly IUserService userService;
        private readonly ISalesmanService salesmanService;
        private readonly INotificationService notificationService;
        private readonly IHtmlSanitizingService htmlSanitizingService;

        public VehiclesController(IVehicleService vehicleService,
            IOfferService offerService,
            IUserService userService,
            ISalesmanService salesmanService,
            INotificationService notificationService,
            IHtmlSanitizingService htmlSanitizingService)
        {
            this.vehicleService = vehicleService;
            this.offerService = offerService;
            this.userService = userService;
            this.salesmanService = salesmanService;
            this.notificationService = notificationService;
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

        public async Task<IActionResult> MyImportedVehicles([FromQuery] VehiclesQueryModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            var queryResult = await vehicleService.GetImporterVehiclesAsync(
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
        public async Task<IActionResult> Buy(int id)
        {
            try
            {
                var vehicle = await vehicleService.GetVehicleByIdAsync(id);
                var currUser = await userService.GetUserByIdAsync(User.Id());
                var salesmanUserId = await salesmanService.GetSalesmanUserIdAsync((int)vehicle.SalesmanId!);
                await vehicleService.BuyVehicleFromSalesmanAsync(id, User.Id());
                await notificationService.CreateNotificationAsync(salesmanUserId,
                    $"Your have sold {vehicle.Name} to {currUser.FirstName} {currUser.LastName} for {vehicle.Price}!",
                    $"Vehicles/Details/{id}");
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpPost]
        public async Task<IActionResult> Edit(VehicleEditModel model)
        {
            model = htmlSanitizingService.SanitizeObject(model);
            try
            {
                if (model.OldPrice < model.Price)
                {
                    //model = await vehicleService.CreateVehicleSellModelAsync(model.Id);
                    return View(model);
                }
                await vehicleService.EditVehicleAsync(model);
            }
            catch (Exception e)
            {

            }
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Import()
        {
            var model = await vehicleService.CreateVehicleImportModelAsync(User.Id());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Import(VehicleImportModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await vehicleService.CreateVehicleImportModelAsync(User.Id());
                return View(model);
            }
            await vehicleService.ImportVehicleAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
