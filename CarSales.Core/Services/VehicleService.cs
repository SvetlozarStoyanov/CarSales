using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
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
                    SalesmanId = v.SalesmanId,
                    SalesmanName = v.Salesman != null ? $"{v.Salesman.User.FirstName} {v.Salesman.User.LastName}" : null,
                    OwnerId = v.OwnerId,
                    OwnerUserId = v.Owner != null ? v.Owner.UserId : null,
                    OwnerName = v.Owner != null ? $"{v.Owner.User.FirstName} {v.Owner.User.LastName}" : null
                }).FirstOrDefaultAsync();
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
                .Where(v => v.OwnerId == salesman.Id).
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
    }
}
