using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Exceptions;
using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository repository;
        public VehicleService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<VehiclesQueryModel> GetAllVehiclesForSaleAsync(string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string? selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically
            )
        {

            var vehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.SalesmanId != null)
                .Include(v => v.Salesman)
                .ThenInclude(s => s.User)
                .ToListAsync();
            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.Contains(searchTerm)
                    || v.Model.Contains(searchTerm))
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
                switch (sorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.VehicleRating).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.VehicleRating).ToList();
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
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman.UserId,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetImportedVehicles(string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically
            )
        {
            var vehicles = await repository.AllReadOnly<Vehicle>()
            .Where(v => v.ImporterId != null)
            .Include(v => v.Importer)
            .ThenInclude(i => i.User)
            .ToListAsync();
            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.Contains(searchTerm)
                    || v.Model.Contains(searchTerm))
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
                switch (sorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.VehicleRating).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.VehicleRating).ToList();
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
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    ImporterId = v.ImporterId,
                    ImporterUserId = v.Importer.UserId,
                    ImporterName = $"{v.Importer.User.FirstName} {v.Importer.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehicleViewModel> GetVehicleByIdAsync(int id)
        {
            var vehicle = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Id == id)
                .Select(v => new VehicleViewModel()
                {
                    Id = v.Id,
                    Brand = v.Brand,
                    Model = v.Model,
                    Description = v.Description,
                    ImageUrl = v.ImageUrl,
                    YearProduced = v.YearProduced,
                    TopSpeed = v.TopSpeed,
                    KilometersDriven = v.KilometersDriven,
                    Price = v.Price,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    OwnerId = v.OwnerId,
                    OwnerUserId = v.Owner != null ? v.Owner.UserId : null,
                    OwnerName = v.Owner != null ? $"{v.Owner.User.FirstName} {v.Owner.User.LastName}" : null,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman != null ? v.Salesman.UserId : null,
                    SalesmanName = v.Salesman != null ? $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}" : null,
                    ImporterId = v.ImporterId,
                    ImporterUserId = v.Importer != null ? v.Importer.UserId : null,
                    ImporterName = v.Importer != null ? $"{v.Importer.User.FirstName} {v.Importer.User.LastName}" : null,
                    VehicleChangeRatingModel = new VehicleChangeRatingModel(v.Id, v.VehicleRating)
                })
                .FirstOrDefaultAsync();

            return vehicle;
        }

        public async Task<VehiclesQueryModel> GetOwnerVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically)
        {
            var vehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Owner.UserId == userId)
                .Include(v => v.Owner)
                .ThenInclude(o => o.User)
                .ToListAsync();

            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.Contains(searchTerm)
                    || v.Model.Contains(searchTerm))
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
                switch (sorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.VehicleRating).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.VehicleRating).ToList();
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
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    OwnerId = v.OwnerId,
                    OwnerUserId = v.Owner.UserId,
                    OwnerName = $"{v.Owner.User.FirstName} {v.Owner.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetSalesmanVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically)
        {
            var vehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Salesman.UserId == userId)
                .Include(v => v.Salesman)
                .ThenInclude(s => s.User)
                .ToListAsync();

            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.Contains(searchTerm)
                    || v.Model.Contains(searchTerm))
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
                switch (sorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.VehicleRating).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.VehicleRating).ToList();
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
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman.UserId,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, sortedVehicles, vehicles.Count);

            return queryModel;
        }

        public async Task<VehiclesQueryModel> GetImporterVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically)
        {
            var vehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Salesman.UserId == userId)
                .Include(v => v.Salesman)
                .ThenInclude(s => s.User)
                .ToListAsync();

            if (vehicles.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    vehicles = vehicles.Where(v => v.Brand.Contains(searchTerm)
                    || v.Model.Contains(searchTerm))
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
                switch (sorting)
                {
                    case VehicleSorting.Alphabetically:
                        vehicles = vehicles.OrderBy(v => v.Brand).ToList();
                        break;
                    case VehicleSorting.PriceAscending:
                        vehicles = vehicles.OrderBy(v => v.Price).ToList();
                        break;
                    case VehicleSorting.PriceDescending:
                        vehicles = vehicles.OrderByDescending(v => v.Price).ToList();
                        break;
                    case VehicleSorting.RatingAscending:
                        vehicles = vehicles.OrderBy(v => v.VehicleRating).ToList();
                        break;
                    case VehicleSorting.RatingDescending:
                        vehicles = vehicles.OrderByDescending(v => v.VehicleRating).ToList();
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
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanId = v.SalesmanId,
                    SalesmanUserId = v.Salesman.UserId,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                });
            if (!sortedVehicles.Any())
            {
                currentPage = 1;
            }
            var queryModel = CreateVehiclesQueryModel(searchTerm, vehiclesPerPage, currentPage, selectedVehicleTypes, sortedVehicles, vehicles.Count);

            return queryModel;
        }


        public async Task ChangeVehicleRatingAsync(int id, int newRating)
        {
            var vehicle = await repository.GetByIdAsync<Vehicle>(id);
            vehicle.VehicleRating = (VehicleRating)newRating;
            repository.Update<Vehicle>(vehicle);
            await repository.SaveChangesAsync();
        }

        public async Task BuyVehicleFromSalesmanAsync(int id, string buyerUserId)
        {
            var vehicle = await repository.All<Vehicle>()
                .Where(v => v.Id == id)
                .Include(v => v.Salesman)
                .ThenInclude(sm => sm.User)
                .FirstOrDefaultAsync();
            var salesman = vehicle.Salesman;
            var buyer = await repository.All<Owner>()
                .Where(b => b.UserId == buyerUserId)
                .Include(o => o.User)
                .FirstOrDefaultAsync();

            if (buyer.User.Credits < vehicle.Price)
            {
                throw new InsufficientCreditsException("You do not have enough credits to purchase this item!");
            }
            if (salesman == null)
            {
                throw new ArgumentNullException("Item is not for sale!");
            }
            vehicle.OwnerId = buyer.Id;
            vehicle.SalesmanId = null;

            buyer.User.Credits -= vehicle.Price;
            salesman.User.Credits += vehicle.Price;
            var sale = new Sale()
            {
                OwnerId = buyer.Id,
                SalesmanId = salesman.Id,
                VehicleId = vehicle.Id,
                SalePrice = vehicle.Price,
            };
            await repository.AddAsync<Sale>(sale);
            await repository.SaveChangesAsync();
        }

        public async Task BuyVehicleFromImporterAsync(int id, string buyerUserId)
        {
            var vehicle = await repository.All<Vehicle>()
                .Where(v => v.Id == id)
                .Include(v => v.Importer)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync();
            var importer = vehicle.Importer;
            var buyer = await repository.All<Owner>()
                .Where(b => b.UserId == buyerUserId)
                .Include(o => o.User)
                .FirstOrDefaultAsync();

            if (buyer.User.Credits < vehicle.Price)
            {
                throw new InsufficientCreditsException("You do not have enough credits to purchase this item!");
            }
            if (importer == null)
            {
                throw new ArgumentNullException("Item is not for sale!");
            }
            vehicle.OwnerId = buyer.Id;
            vehicle.ImporterId = null;

            buyer.User.Credits -= vehicle.Price;
            importer.User.Credits += vehicle.Price;
            var sale = new Sale()
            {
                OwnerId = buyer.Id,
                ImporterId = importer.Id,
                VehicleId = vehicle.Id,
                SalePrice = vehicle.Price,
            };
            await repository.AddAsync<Sale>(sale);
            await repository.SaveChangesAsync();
        }

        public async Task<VehicleSellModel> CreateVehicleSellModelAsync(int id)
        {
            var vehicle = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.Id == id)
                .Select(v => new VehicleSellModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model}",
                    Description = v.Description,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    OldPrice = v.Price,
                    OwnerUserId = v.Owner.UserId,
                    //VehicleRatings = Enum.GetValues<VehicleRating>()
                })
                .FirstOrDefaultAsync();
            var vehicleRatings = Enum.GetValues<VehicleRating>().ToArray();
            vehicle.VehicleRatings = vehicleRatings;
            return vehicle;
        }

        public async Task PutVehicleForSaleAsync(VehicleSellModel model)
        {
            var vehicle = await repository.All<Vehicle>()
                .Where(v => v.Id == model.Id)
                .Include(v => v.Owner)
                .ThenInclude(sm => sm.User)
                .FirstOrDefaultAsync();
            var salesman = await repository.All<Salesman>()
                .Where(s => s.User.Id == vehicle.Owner.UserId)
                .FirstOrDefaultAsync();

            vehicle.Description = model.Description;
            vehicle.Price = model.Price;
            vehicle.VehicleRating = model.VehicleRating;
            vehicle.OwnerId = null;
            vehicle.SalesmanId = salesman.Id;

            await repository.SaveChangesAsync();
        }

        public async Task EditVehicleForSaleAsync(VehicleSellModel model)
        {
            var vehicle = await repository.All<Vehicle>()
                .Where(v => v.Id == model.Id)
                .Include(v => v.Owner)
                .ThenInclude(sm => sm.User)
                .FirstOrDefaultAsync();

            vehicle.Description = model.Description;
            vehicle.Price = model.Price;
            vehicle.VehicleRating = model.VehicleRating;

            await repository.SaveChangesAsync();
        }

        public async Task<VehicleImportModel> CreateVehicleImportModelAsync(string userId)
        {
            var importer = await repository.AllReadOnly<Importer>()
                .FirstOrDefaultAsync(i => i.UserId == userId);
            var vehicle = new VehicleImportModel()
            {
                ImporterId = importer.Id,
                VehicleTypes = Enum.GetValues<VehicleType>(),
                VehicleRatings = Enum.GetValues<VehicleRating>()
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
                KilometersDriven = model.KilometersDriven,
                Price = model.Price,
                VehicleType = model.VehicleType,
                VehicleRating = model.VehicleRating,
                ImporterId = model.ImporterId
            };
            var importer = await repository.All<Importer>()
                .Where(i => i.Id == model.ImporterId)
                .Include(i => i.User)
                .FirstOrDefaultAsync();
            importer.User.Credits -= model.Price * 0.90m;
            await repository.AddAsync<Vehicle>(vehicle);
            await repository.SaveChangesAsync();
        }

        private VehiclesQueryModel CreateVehiclesQueryModel(string? searchTerm, int vehiclesPerPage, int currentPage, string? selectedVehicleTypes, IEnumerable<VehicleListModel> vehicles, int count)
        {
            var model = new VehiclesQueryModel()
            {
                SearchTerm = searchTerm,
                VehiclesPerPage = vehiclesPerPage,
                CurrentPage = currentPage,
                SelectedVehicleTypes = selectedVehicleTypes,
                SortingOptions = Enum.GetValues<VehicleSorting>().ToHashSet(),
                VehicleTypes = Enum.GetValues<VehicleType>().ToHashSet(),
                VehiclesCount = count,
                Vehicles = vehicles
            };
            return model;
        }
    }
}
