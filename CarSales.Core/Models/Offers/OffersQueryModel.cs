using CarSales.Core.Enums;
using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Offers
{
    public class OffersQueryModel
    {
        public OffersQueryModel()
        {
            VehicleNames = new HashSet<string>();
            OfferStatuses = Enum.GetValues<OfferStatus>().ToHashSet();
            SortingOptions = Enum.GetValues<OfferSorting>().ToHashSet();
            Offers = new List<OfferListModel>();
        }
        public int CurrentPage { get; set; } = 1;
        public int OffersPerPage { get; set; } = 6;
        public int FirstPage { get; set; } = 1;
        public int MaxPage { get; set; }
        public int OfferCount { get; set; }
        public string? VehicleName { get; set; }
        public string? SalesmanName { get; set; }
        public string? OfferorName { get; set; }
        public OfferStatus OfferStatus{ get; set; }
        public OfferSorting OfferSorting { get; set; }
        public ICollection<string> VehicleNames { get; set; }
        public ICollection<string> SalesmenNames { get; set; }
        public ICollection<string> OfferorNames { get; set; } = null!;
        public IEnumerable<int> PreviousPages { get; set; } = null!;
        public IEnumerable<int> NextPages { get; set; } = null!;
        public ICollection<OfferStatus> OfferStatuses { get; set; } = null!;
        public ICollection<OfferSorting> SortingOptions { get; set; } = null!;
        public IEnumerable<OfferListModel> Offers { get; set; } = null!;
    }
}
