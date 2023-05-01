using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
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
        public async Task<ICollection<VehicleListViewModel>> GetAllVehiclesWhichAreForSaleAsync()
        {
            var vehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.OwnerId == null)
                .Select(v => new VehicleListViewModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                })
                .ToListAsync();
            return vehicles;
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
                    VehicleChangeRatingModel = new VehicleChangeRatingModel(v.Id, v.VehicleRating)
                })
                .FirstOrDefaultAsync();

            return vehicle;
        }

        public async Task<ICollection<VehicleListViewModel>> GetOwnerVehiclesAsync(string userId)
        {
            var owner = await repository.AllReadOnly<Owner>()
                .FirstOrDefaultAsync(o => o.UserId == userId);
            var ownerVehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.OwnerId == owner.Id)
                .Select(v => new VehicleListViewModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                }).ToListAsync();
            return ownerVehicles;
        }

        public async Task<ICollection<VehicleListViewModel>> GetSalesmanVehiclesAsync(string userId)
        {
            var salesman = await repository.AllReadOnly<Salesman>()
                .FirstOrDefaultAsync(o => o.UserId == userId);
            var salesmanVehicles = await repository.AllReadOnly<Vehicle>()
                .Where(v => v.SalesmanId == salesman.Id).
                Select(v => new VehicleListViewModel()
                {
                    Id = v.Id,
                    Name = $"{v.Brand} {v.Model} {v.YearProduced}",
                    ImageUrl = v.ImageUrl,
                    VehicleType = v.VehicleType,
                    VehicleRating = v.VehicleRating,
                    Price = v.Price,
                    SalesmanName = $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}"
                }).ToListAsync();
            return salesmanVehicles;
        }



        public async Task ChangeVehicleRatingAsync(int id, int newRating)
        {
            var vehicle = await repository.GetByIdAsync<Vehicle>(id);
            vehicle.VehicleRating = (VehicleRating)newRating;
            repository.Update<Vehicle>(vehicle);
            await repository.SaveChangesAsync();
        }

        public async Task BuyVehicleAsync(int id, string buyerUserId)
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



        public async Task<VehicleSellModel> CreateVehicleSellModel(int id)
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
    }
}
