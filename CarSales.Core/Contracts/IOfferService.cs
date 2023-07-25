using CarSales.Core.Enums;
using CarSales.Core.Models.Offers;
using CarSales.Infrastructure.Data.Entities;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Contracts
{
    public interface IOfferService
    {
        /// <summary>
        /// Returns true if <see cref="User"/> with <paramref name="userId"/> has not already created an offer
        /// for <see cref="Vehicle"/> with given <paramref name="vehicleId"/>, returns false otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vehicleId"></param>
        /// <returns><see cref="bool"/></returns>
        
        Task<bool> CanCreateOfferAsync(string userId, int vehicleId);

        /// <summary>
        /// Returns true if <see cref="User"/> with <paramref name="userId"/> has created or received an <see cref="Offer"/>
        /// with given <paramref name="offerId"/>, returns false otherwise
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="offerId"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> CanViewOfferAsync(string userId, int offerId);

        /// <summary>
        ///Returns true if <see cref = "User" /> with < paramref name="userId"/> has received an <see cref="Offer"/>
        /// with given <paramref name="offerId"/>, returns false otherwise
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="offerId"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> CanRespondToOfferAsync(string userId, int offerId);

        /// <summary>
        /// Returns the <see cref="Offer.Id"/> of <see cref="Offer"/> with given <paramref name="vehicleId"/> and  
        /// <see cref="Offer.Offeror"/> with given <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vehicleId"></param>
        /// <returns><see cref="int"/></returns>
        Task<int> GetOfferIdAsync(string userId, int vehicleId);

        /// <summary>
        /// Returns all <see cref="Offer"/> of <see cref="Owner"/> with <paramref name="userId"/>
        /// which match the given criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentPage"></param>
        /// <param name="offersPerPage"></param>
        /// <param name="vehicleName"></param>
        /// <param name="offerSorting"></param>
        /// <returns><see cref="OffersQueryModel"/></returns>
        Task<OffersQueryModel> GetOwnerOffersAsync(string userId,
            int currentPage = 1,
            int offersPerPage = 6,
            string? vehicleName = null,
            string? salesmanName = null,
            OfferSorting offerSorting = OfferSorting.Newest);

        /// <summary>
        /// Returns all Offers of <see cref="Salesman"/> with <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OffersQueryModel> GetSalesmanOffersAsync(string userId,
            int currentPage = 1,
            int offersPerPage = 6,
            string? vehicleName = null,
            string? offerorName = null,
            OfferSorting offerSorting = OfferSorting.Newest);

        /// <summary>
        /// Returns <see cref="Offer"/> with given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="OfferViewModel"/></returns>
        Task<OfferViewModel> GetOfferByIdAsync(int id);

        /// <summary>
        /// Creates <see cref="OfferCreateModel"/> for creating a <see cref="Offer"/>. Sets <see cref="OfferCreateModel.OfferorId"/> to 
        /// <see cref="Owner.Id"/> of <see cref="User"/> with <paramref name="userId"/>
        /// and sets <see cref="OfferCreateModel.VehicleId"/> to <paramref name="vehicleId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vehicleId"></param>
        /// <returns><see cref="OfferCreateModel"/></returns>
        Task<OfferCreateModel> CreateOfferCreateModelAsync(string userId, int vehicleId);

        /// <summary>
        /// Creates <see cref="Offer"/> from <paramref name="model"/>
        /// and adds it to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateOfferAsync(OfferCreateModel model);

        /// <summary>
        /// Creates an <see cref="OfferEditModel"/> from <see cref="Offer"/>
        /// with given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OfferEditModel> CreateOfferEditModelAsync(int id);

        /// <summary>
        /// Edits <see cref="Offer"/> with <paramref name="model"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditOfferAsync(OfferEditModel model);

        /// <summary>
        /// Adds <see cref="Offer.Price"/> credits to <see cref="Offer.Salesman"/> and subtracts them
        /// from <see cref="Offer.Offeror"/>. Changes <see cref="Offer"/> to <see cref="OfferStatus.Accepted"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task AcceptOfferAsync(int id);

        /// <summary>
        /// Changes <see cref="Offer"/> to <see cref="OfferStatus.Declined"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeclineOfferAsync(int id);

        /// <summary>
        /// Deletes <see cref="Offer"/> with
        /// given <paramref name="id"/> from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task CancelOfferAsync(int id);
    }
}
