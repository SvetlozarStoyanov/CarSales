using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Exceptions;
using CarSales.Core.Models.Reviews;
using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.DataAccess.UnitOfWork;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<VehiclesQueryModel> GetVehiclesForSaleAsync(string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string? selectedVehicleTypes = null,
            VehicleSorting vehicleSorting = VehicleSorting.Alphabetically
            )
        {

            var vehicles = await unitOfWork.VehicleRepository.AllReadOnly()
                .Where(v => v.SalesmanId != null)
                .Include(v => v.Salesman)
                .ThenInclude(s => s.User)
                .Include(v => v.Reviews)
                .ToListAsync();
            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.ToLower().Contains(searchTerm.ToLower())
                    || v.Model.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
                }
                if (!string.IsNullOrEmpty(selectedVehicleTypes))
                {
                    var selectedTypes = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(vt => Enum.Parse<VehicleType>(vt))
                        .ToList();
                    vehicles = vehicles.Where(v => selectedTypes.Contains(v.VehicleType))
                        .ToList();
                }
                switch (vehicleSorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                        ? v.Reviews.Average(r => (int)r.VehicleRating) : 1).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                        ? v.Reviews.Average(r => (int)r.VehicleRating) : 1).ToList();
                        break;
                }
            }

            var sortedVehicles = vehicles
                .Skip((currentPage - 1) * vehiclesPerPage)
                .Take(vehiclesPerPage)
                .Select(v => new VehicleListModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                    VehicleType = v.VehicleType,
                    VehicleRating = v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                    ? (VehicleRating)v.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).Average(r => (int)r.VehicleRating)
                    : VehicleRating.NotRated,
                    Price = v.Price,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman.UserId,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, vehicleSorting, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetImportedVehiclesAsync(string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting vehicleSorting = VehicleSorting.Alphabetically
            )
        {
            var vehicles = await unitOfWork.VehicleRepository.AllReadOnly()
            .Where(v => v.ImporterId != null)
            .Include(v => v.Importer)
            .ThenInclude(i => i.User)
            .Include(v => v.Reviews)

            .ToListAsync();
            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.ToLower().Contains(searchTerm.ToLower())
                    || v.Model.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
                }
                if (!string.IsNullOrEmpty(selectedVehicleTypes))
                {
                    var selectedTypes = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(vt => Enum.Parse<VehicleType>(vt))
                        .ToList();
                    vehicles = vehicles.Where(v => selectedTypes.Contains(v.VehicleType))
                        .ToList();
                }
                switch (vehicleSorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                }
            }
            var sortedVehicles = vehicles
                .Skip((currentPage - 1) * vehiclesPerPage)
                .Take(vehiclesPerPage)
                .Select(v => new VehicleListModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                    VehicleType = v.VehicleType,
                    VehicleRating = VehicleRating.NotRated,
                    Price = v.Price,
                    ImporterId = v.ImporterId,
                    ImporterUserId = v.Importer.UserId,
                    ImporterName = $"{v.Importer.User.FirstName} {v.Importer.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, vehicleSorting, sortedVehicles, vehicles.Count);

            queryModel.SortingOptions = queryModel.SortingOptions.Take(queryModel.SortingOptions.Count() - 2);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetOwnerVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting vehicleSorting = VehicleSorting.Alphabetically)
        {
            var vehicles = await unitOfWork.VehicleRepository.AllReadOnly()
                .Where(v => v.Owner.UserId == userId)
                .Include(v => v.Owner)
                .ThenInclude(o => o.User)
                .Include(v => v.Reviews)
                .ToListAsync();

            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.ToLower().Contains(searchTerm.ToLower())
                    || v.Model.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
                }
                if (!string.IsNullOrEmpty(selectedVehicleTypes))
                {
                    var selectedTypes = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(vt => Enum.Parse<VehicleType>(vt))
                        .ToList();
                    vehicles = vehicles.Where(v => selectedTypes.Contains(v.VehicleType))
                        .ToList();
                }
                switch (vehicleSorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                        ? v.Reviews.Average(r => (int)r.VehicleRating) : 1).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                        ? v.Reviews.Average(r => (int)r.VehicleRating) : 1).ToList();
                        break;
                }
            }

            var sortedVehicles = vehicles
                .Skip((currentPage - 1) * vehiclesPerPage)
                .Take(vehiclesPerPage)
                .Select(v => new VehicleListModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                    VehicleType = v.VehicleType,
                    VehicleRating = v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                    ? (VehicleRating)v.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).Average(r => (int)r.VehicleRating)
                    : VehicleRating.NotRated,
                    Price = v.Price,
                    OwnerId = v.OwnerId,
                    OwnerUserId = v.Owner.UserId,
                    OwnerName = $"{v.Owner.User.FirstName} {v.Owner.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, vehicleSorting, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetSalesmanVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting vehicleSorting = VehicleSorting.Alphabetically)
        {
            var vehicles = await unitOfWork.VehicleRepository.AllReadOnly()
                .Where(v => v.Salesman.UserId == userId)
                .Include(v => v.Salesman)
                .ThenInclude(s => s.User)
                .Include(v => v.Reviews)
                .ToListAsync();

            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.ToLower().Contains(searchTerm.ToLower())
                    || v.Model.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
                }
                if (!string.IsNullOrEmpty(selectedVehicleTypes))
                {
                    var selectedTypes = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(vt => Enum.Parse<VehicleType>(vt))
                        .ToList();
                    vehicles = vehicles.Where(v => selectedTypes.Contains(v.VehicleType))
                        .ToList();
                }
                switch (vehicleSorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                        ? v.Reviews.Average(r => (int)r.VehicleRating) : 1).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                        ? v.Reviews.Average(r => (int)r.VehicleRating) : 1).ToList();
                        break;
                }
            }

            var sortedVehicles = vehicles
                .Skip((currentPage - 1) * vehiclesPerPage)
                .Take(vehiclesPerPage)
                .Select(v => new VehicleListModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                    VehicleType = v.VehicleType,
                    VehicleRating = v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                    ? (VehicleRating)v.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).Average(r => (int)r.VehicleRating)
                    : VehicleRating.NotRated,
                    Price = v.Price,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman.UserId,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, vehicleSorting, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetImporterVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting vehicleSorting = VehicleSorting.Alphabetically)
        {
            var vehicles = await unitOfWork.VehicleRepository.AllReadOnly()
                .Where(v => v.Importer.UserId == userId)
                .Include(v => v.Importer)
                .ThenInclude(s => s.User)
                .Include(v => v.Reviews)
                .ToListAsync();

            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.ToLower().Contains(searchTerm.ToLower())
                    || v.Model.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
                }
                if (!string.IsNullOrEmpty(selectedVehicleTypes))
                {
                    var selectedTypes = selectedVehicleTypes.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Select(vt => Enum.Parse<VehicleType>(vt))
                        .ToList();
                    vehicles = vehicles.Where(v => selectedTypes.Contains(v.VehicleType))
                        .ToList();
                }
                switch (vehicleSorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                }
            }

            var sortedVehicles = vehicles
                .Skip((currentPage - 1) * vehiclesPerPage)
                .Take(vehiclesPerPage)
                .Select(v => new VehicleListModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                    VehicleType = v.VehicleType,
                    VehicleRating = v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                    ? (VehicleRating)v.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).Average(r => (int)r.VehicleRating)
                    : VehicleRating.NotRated,
                    Price = v.Price,
                    SalesmanId = v.SalesmanId,
                    ImporterUserId = v.Importer.UserId,
                    ImporterName = $"{v.Importer.User.FirstName} {v.Importer.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, vehicleSorting, sortedVehicles, vehicles.Count);

            queryModel.SortingOptions = queryModel.SortingOptions.Take(queryModel.SortingOptions.Count() - 2);
            return queryModel;
        }

        public async Task<VehicleViewModel> GetVehicleByIdAsync(int id)
        {
            var vehicle = await unitOfWork.VehicleRepository.AllReadOnly()
                .Where(v => v.Id == id)
                .Select(v => new VehicleViewModel()
                {
                    Id = v.Id,
                    Brand = v.Brand,
                    Model = v.Model,
                    Name = $"{v.Brand} {v.Model}",
                    Description = v.Description,
                    ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                    YearProduced = v.YearProduced,
                    TopSpeed = v.TopSpeed,
                    KilometersDriven = v.KilometersDriven,
                    Price = v.Price,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.Reviews.Count(r => r.ReviewStatus == ReviewStatus.Completed) > 0
                    ? (VehicleRating)v.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).Average(r => (int)r.VehicleRating)
                    : VehicleRating.NotRated,
                    OwnerId = v.OwnerId,
                    OwnerUserId = v.Owner != null ? v.Owner.UserId : null,
                    OwnerName = v.Owner != null ? $"{v.Owner.User.FirstName} {v.Owner.User.LastName}" : null,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman != null ? v.Salesman.UserId : null,
                    SalesmanName = v.Salesman != null ? $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}" : null,
                    ImporterId = v.ImporterId,
                    ImporterUserId = v.Importer != null ? v.Importer.UserId : null,
                    ImporterName = v.Importer != null ? $"{v.Importer.User.FirstName} {v.Importer.User.LastName}" : null,
                    VehicleSellModel = v.OwnerId != null ? new VehicleSellModel()
                    {
                        Id = v.Id,
                        Name = $"{v.Brand} {v.Model}",
                        Description = v.Description,
                        Price = v.Price,
                        OldPrice = v.Price
                    } : null,
                    VehicleBuyModel = (v.SalesmanId != null || v.ImporterId != null) ? new VehicleBuyModel()
                    {
                        Id = v.Id,
                        Name = $"{v.Brand} {v.Model}",
                        Price = v.Price
                    } : null,
                    VehicleEditModel = (v.SalesmanId != null || v.ImporterId != null) ? new VehicleEditModel()
                    {
                        Id = v.Id,
                        Name = $"{v.Brand} {v.Model}",
                        Description = v.Description,
                        ImageUrl = v.ImageUrl ?? $"/img/VehicleTypes/{v.VehicleType}.png",
                        DefaultImageUrl = $"/img/VehicleTypes/{v.VehicleType}.png",
                        Price = v.Price,
                        OldPrice = v.Price
                    } : null,
                    Reviews = v.Reviews.Where(r => r.ReviewStatus == ReviewStatus.Completed).OrderByDescending(r => r.Id).Take(3).Select(r => new ReviewMinModel()
                    {
                        Id = r.Id,
                        Title = r.Title,
                        VehicleRating = r.VehicleRating,
                        ReviewerName = r.Reviewer.User.UserName
                    }).ToList()

                })
                .FirstOrDefaultAsync();

            return vehicle;
        }

        public async Task BuyVehicleFromSalesmanAsync(int id, string buyerUserId)
        {
            var vehicle = await unitOfWork.VehicleRepository.All()
                .Where(v => v.Id == id)
                .Include(v => v.Salesman)
                .ThenInclude(sm => sm.User)
                .Include(v => v.Reviews)
                .FirstOrDefaultAsync();
            var salesman = vehicle!.Salesman;
            var buyer = await unitOfWork.OwnerRepository.All()
                .Where(b => b.UserId == buyerUserId)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();
            var buyerAvailableCredits = buyer.User.Credits - buyer.Offers.Where(o => o.Status == OfferStatus.Pending && o.VehicleId != id).Sum(o => o.Price);
            if (buyerAvailableCredits < vehicle.Price)
            {
                throw new InsufficientCreditsException("You do not have enough credits to purchase this item!");
            }
            if (salesman == null)
            {
                throw new NotForSaleException("Item is not for sale!");
            }
            vehicle.OwnerId = buyer.Id;
            vehicle.SalesmanId = null;

            buyer.User.Credits -= vehicle.Price;
            salesman.User.Credits += vehicle.Price;
            var sale = new Sale()
            {
                SalePrice = vehicle.Price,
                VehiclePrice = vehicle.Price,
                OwnerId = buyer.Id,
                SalesmanId = salesman.Id,
                VehicleId = vehicle.Id,
            };

            await DeclineAllOffersForVehicleAsync(id);
            await unitOfWork.SaleRepository.AddAsync(sale);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task BuyVehicleFromImporterAsync(int id, string buyerUserId)
        {
            var vehicle = await unitOfWork.VehicleRepository.All()
                .Where(v => v.Id == id)
                .Include(v => v.Importer)
                .ThenInclude(i => i!.User)
                .FirstOrDefaultAsync();
            var importer = vehicle!.Importer;
            var buyer = await unitOfWork.OwnerRepository.All()
                .Where(b => b.UserId == buyerUserId)
                .Include(o => o.User)
                .Include(o => o.Offers)
                .FirstOrDefaultAsync();
            var buyerAvailableCredits = buyer!.User.Credits - buyer.Offers
                .Where(o => o.Status == OfferStatus.Pending)
                .Sum(o => o.Price);
            if (buyerAvailableCredits < vehicle.Price)
            {
                throw new InsufficientCreditsException("You do not have enough credits to purchase this item!");
            }
            if (importer == null)
            {
                throw new NotForSaleException("Item is not for sale!");
            }
            vehicle.OwnerId = buyer.Id;
            vehicle.ImporterId = null;

            buyer.User.Credits -= vehicle.Price;
            importer.User.Credits += vehicle.Price;
            var sale = new Sale()
            {
                SalePrice = vehicle.Price,
                VehiclePrice = vehicle.Price,
                OwnerId = buyer.Id,
                ImporterId = importer.Id,
                VehicleId = vehicle.Id,
            };
            await unitOfWork.SaleRepository.AddAsync(sale);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<VehicleSellModel> CreateVehicleSellModelAsync(int id)
        {
            var vehicle = await unitOfWork.VehicleRepository.AllReadOnly()
                .Where(v => v.Id == id)
                .Select(v => new VehicleSellModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model}",
                    Description = v.Description,
                    Price = v.Price,
                    OldPrice = v.Price,
                    OwnerUserId = v.Owner.UserId,
                })
                .FirstOrDefaultAsync();
            return vehicle;
        }

        public async Task PutVehicleForSaleAsync(VehicleSellModel model)
        {
            var vehicle = await unitOfWork.VehicleRepository.All()
                .Where(v => v.Id == model.Id)
                .Include(v => v.Owner)
                .ThenInclude(sm => sm.User)
                .FirstOrDefaultAsync();

            var salesman = await unitOfWork.SalesmanRepository.All()
                .Where(s => s.User.Id == vehicle.Owner.UserId)
                .FirstOrDefaultAsync();

            vehicle.Description = model.Description;
            vehicle.Price = model.Price;
            var sale = new Sale()
            {
                SalePrice = 0,
                VehiclePrice = vehicle.Price,
                VehicleId = vehicle.Id,
                SalesmanId = salesman.Id,
                OwnerId = vehicle.Owner.Id,
            };
            vehicle.OwnerId = null;
            vehicle.SalesmanId = salesman.Id;
            await unitOfWork.SaleRepository.AddAsync(sale);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task EditVehicleAsync(VehicleEditModel model)
        {
            var vehicle = await unitOfWork.VehicleRepository.All()
                .Where(v => v.Id == model.Id)
                .Include(v => v.Owner)
                .ThenInclude(sm => sm.User)
                .FirstOrDefaultAsync();

            vehicle.Description = model.Description;
            vehicle.Price = model.Price;
            vehicle.ImageUrl = model.ImageUrl;

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<VehicleImportModel> CreateVehicleImportModelAsync(string userId)
        {
            var importer = await unitOfWork.ImporterRepository.AllReadOnly()
                .FirstOrDefaultAsync(i => i.UserId == userId);
            var vehicle = new VehicleImportModel()
            {
                ImporterId = importer.Id,
                VehicleTypes = Enum.GetValues<VehicleType>(),
            };
            return vehicle;
        }

        public async Task ImportVehicleAsync(VehicleImportModel model)
        {
            var vehicle = new Vehicle()
            {
                Brand = model.Brand,
                Model = model.ModelName,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                YearProduced = model.YearProduced,
                TopSpeed = model.TopSpeed,
                Price = model.Price,
                VehicleType = model.VehicleType,
                ImporterId = model.ImporterId
            };
            var importer = await unitOfWork.ImporterRepository.All()
                .Where(i => i.Id == model.ImporterId)
                .Include(i => i.User)
                .FirstOrDefaultAsync();
            importer.User.Credits -= model.Price * 0.90m;
            await unitOfWork.VehicleRepository.AddAsync(vehicle);
            await unitOfWork.SaveChangesAsync();
        }

        private VehiclesQueryModel CreateVehiclesQueryModel(string? searchTerm, int vehiclesPerPage, int currentPage, string? selectedVehicleTypes, VehicleSorting vehicleSorting, IEnumerable<VehicleListModel> vehicles, int vehicleCount)
        {
            var model = new VehiclesQueryModel()
            {
                SearchTerm = searchTerm,
                VehiclesPerPage = vehiclesPerPage,
                CurrentPage = currentPage,
                MaxPage = (int)Math.Ceiling(vehicleCount / (double)vehiclesPerPage),
                SelectedVehicleTypes = selectedVehicleTypes,
                VehicleSorting = vehicleSorting,
                SortingOptions = Enum.GetValues<VehicleSorting>().ToHashSet(),
                VehicleTypes = Enum.GetValues<VehicleType>().ToHashSet(),
                VehicleCount = vehicleCount,
                Vehicles = vehicles
            };
            if (model.MaxPage > 1)
            {
                var previousPages = new HashSet<int>();
                var nextPages = new HashSet<int>();
                //var pagesToMaxPage = model.MaxPage - model.CurrentPage;
                var numberOfPages = 0;
                var index = 1;
                while (numberOfPages < 4 && numberOfPages < model.MaxPage - 1)
                {
                    var previousPage = model.CurrentPage - index;
                    var nextPage = model.CurrentPage + index;
                    if (previousPage >= 1)
                    {
                        previousPages.Add(previousPage);
                        numberOfPages++;
                    }
                    if (nextPage <= model.MaxPage)
                    {
                        nextPages.Add(nextPage);
                        numberOfPages++;
                    }
                    index++;
                }
                model.PreviousPages = previousPages.Reverse();
                model.NextPages = nextPages;
            }
            return model;
        }

        private async Task DeclineAllOffersForVehicleAsync(int vehicleId)
        {
            var offers = await unitOfWork.OfferRepository.All()
                .Where(o => o.VehicleId == vehicleId && o.Status == OfferStatus.Pending)
                .ToListAsync();
            foreach (var offer in offers)
            {
                offer.Status = OfferStatus.Declined;
            }
        }
    }
}
