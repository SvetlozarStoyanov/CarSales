using CarSales.Core.Models.Offers;

namespace CarSales.Core.Contracts
{
    public interface IOfferService
    {
        Task<bool> CanCreateOfferAsync(string userId, int vehicleId);


        Task<bool> CanViewOfferAsync(string userId, int offerId);


        Task<bool> CanRespondToOfferAsync(string userId, int offerId);

        Task<int> GetOfferIdAsync(string userId, int vehicleId);

        Task<IEnumerable<OfferListModel>> GetOwnerOffersAsync(string userId);


        Task<IEnumerable<OfferListModel>> GetSalesmanOffersAsync(string userId);


        Task<OfferViewModel> GetOfferByIdAsync(int id);


        Task<OfferCreateModel> CreateOfferCreateModelAsync(string userId, int vehicleId);


        Task CreateOfferAsync(OfferCreateModel model);


        Task<OfferEditModel> CreateOfferEditModelAsync(int id);


        Task EditOfferAsync(OfferEditModel model);


        Task AcceptOfferAsync(int id);


        Task DeclineOfferAsync(int id);


        Task CancelOfferAsync(int id);
    }
}
