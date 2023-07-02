using CarSales.Core.Enums;
using CarSales.Core.Models.Vehicles;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IVehicleService
    {
        /// <summary>
        /// Returns all <see cref="Vehicle"/> which match the given criteria
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="vehiclesPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="sorting"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <returns><see cref="VehiclesQueryModel"/></returns>
        Task<VehiclesQueryModel> GetAllVehiclesForSaleAsync(string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically
            );

        /// <summary>
        /// Returns all imported <see cref="Vehicle"/> which match the given criteria
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="vehiclesPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="sorting"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <returns><see cref="VehiclesQueryModel"/></returns>
        Task<VehiclesQueryModel> GetImportedVehicles(string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically
            );

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
        /// <param name="searchTerm"></param>
        /// <param name="vehiclesPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <param name="sorting"></param>
        /// <returns><see cref="VehiclesQueryModel"/></returns>
        Task<VehiclesQueryModel> GetOwnerVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically);

        /// <summary>
        /// Returns all <see cref="Vehicle"/> being sold by <see cref="Salesman"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchTerm"></param>
        /// <param name="vehiclesPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <param name="sorting"></param>
        /// <returns><see cref="VehiclesQueryModel"/></returns>
        Task<VehiclesQueryModel> GetSalesmanVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically);

        /// <summary>
        /// Returns all <see cref="Vehicle"/> imported by <see cref="Importer"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchTerm"></param>
        /// <param name="vehiclesPerPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="selectedVehicleTypes"></param>
        /// <param name="sorting"></param>
        /// <returns><see cref="VehiclesQueryModel"/></returns>
        Task<VehiclesQueryModel> GetImporterVehiclesAsync(string userId,
            string searchTerm = null,
            int vehiclesPerPage = 6,
            int currentPage = 1,
            string selectedVehicleTypes = null,
            VehicleSorting sorting = VehicleSorting.Alphabetically);


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

        /// <summary>
        /// Creates <see cref="VehicleImportModel"/> and sets the <see cref="VehicleImportModel.ImporterId"/>
        /// to <see cref="Importer.Id"/> of <see cref="User"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="VehicleImportModel"/></returns>
        Task<VehicleImportModel> CreateVehicleImportModelAsync(string userId);

        /// <summary>
        /// Creates a <see cref="Vehicle"/> from given <paramref name="model"/>
        /// and adds it to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ImportVehicleAsync(VehicleImportModel model);

    }
}
