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
        Task<ICollection<VehicleListViewModel>> GetAllVehiclesForSaleAsync();

        /// <summary>
        /// Returns all <see cref="Vehicle"/> which are for imported
        /// </summary>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetAllImportedVehiclesAsync();

        /// <summary>
        /// Returns <see cref="Vehicle"/> by given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="VehicleViewModel"/></returns>
        Task<VehicleViewModel> GetVehicleByIdAsync(int id);

        /// <summary>
        /// Returns all <see cref="Vehicle"/>belonging to <see cref="Owner"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetOwnerVehiclesAsync(string userId);

        /// <summary>
        /// Returns all <see cref="Vehicle"/> belonging to <see cref="Salesman"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetSalesmanVehiclesAsync(string userId);

        /// <summary>
        /// Returns all <see cref="Vehicle"/> belonging to <see cref="Importer"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="ICollection{VehicleListViewModel}"/></returns>
        Task<ICollection<VehicleListViewModel>> GetImporterVehiclesAsync(string userId);

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
        Task BuyVehicleFromSalesmanAsync(int id, string buyerUserId);

        /// <summary>
        /// <see cref="Vehicle"/> with <paramref name="id"/> is purchased by 
        /// <see cref="Owner"/> with <paramref name="buyerUserId"/> and added to his <see cref="Owner.Vehicles"/> collection
        /// <see cref="User.Credits"/> are transferred from <see cref="Owner"/> to <see cref="Importer"/> 
        /// who was selling the <see cref="Vehicle"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buyerUserId"></param>
        /// <returns></returns>
        Task BuyVehicleFromImporterAsync(int id, string buyerUserId);

        /// <summary>
        /// Creates <see cref="VehicleSellModel"/> from <see cref="Vehicle"/> with given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="VehicleSellModel"/></returns>
        Task<VehicleSellModel> CreateVehicleSellModelAsync(int id);

        /// <summary>
        /// Removes <see cref="Vehicle.OwnerId"/> from <see cref="Vehicle"/> and sets <see cref="Vehicle.SalesmanId"/>
        /// to <see cref="Vehicle"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task PutVehicleForSaleAsync(VehicleSellModel model);

        /// <summary>
        /// Edits <see cref="Vehicle"/> with parameters from <paramref name="model"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditVehicleForSaleAsync(VehicleSellModel model);


        Task<VehicleImportModel> CreateVehicleImportModelAsync(string userId);


        Task ImportVehicleAsync(VehicleImportModel model);
    }
}
