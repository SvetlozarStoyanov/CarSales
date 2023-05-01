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

        /// <summary>
        /// Changes the <see cref="VehicleRating"/> of vehicle with given <paramref name="id"/>
        /// with <paramref name="newRating"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newRating"></param>
        /// <returns></returns>
        Task ChangeVehicleRatingAsync(int id, int newRating);

        /// <summary>
        /// <see cref="Vehicle"/> with <paramref name="id"/> is purchased by 
        /// <see cref="Owner"/> with <paramref name="buyerUserId"/> and added to his <see cref="Owner.Vehicles"/> collection
        /// <see cref="User.Credits"/> are transferred from <see cref="Owner"/> to <see cref="Salesman"/> 
        /// who was selling the <see cref="Vehicle"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buyerUserId"></param>
        /// <returns></returns>
        Task BuyVehicleAsync(int id, string buyerUserId);

        Task<VehicleSellModel> CreateVehicleSellModel(int id);

        Task PutVehicleForSaleAsync(VehicleSellModel model);
    }
}
