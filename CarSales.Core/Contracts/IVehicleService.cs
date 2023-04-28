using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IVehicleService
    {
        /// <summary>
        /// Returns all <see cref="Vehicle"/> which are for sale
        /// </summary>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetAllVehiclesWhichAreForSaleAsync();

        /// <summary>
        /// Returns <see cref="Vehicle"/> by given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="VehicleViewModel"/></returns>
        Task<VehicleViewModel> GetVehicleByIdAsync(int id);

        /// <summary>
        /// Returns all <see cref="Vehicle"/> to <see cref="Owner"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetOwnerVehiclesAsync(string userId);

        /// <summary>
        /// Returns all <see cref="Vehicle"/> to <see cref="Salesman"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetSalesmanVehiclesAsync(string userId);

        Task ChangeVehicleRatingAsync(int id, int newRating);
    }
}
