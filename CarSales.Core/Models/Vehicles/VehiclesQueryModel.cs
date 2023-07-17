using CarSales.Core.Enums;
using CarSales.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Core.Models.Vehicles
{
    public class VehiclesQueryModel
    {
        public VehiclesQueryModel()
        {
            SortingOptions = new HashSet<VehicleSorting>();
            VehicleTypes = new HashSet<VehicleType>();
            //SelectedVehicleTypes = new HashSet<VehicleType>();
            Vehicles = new List<VehicleListModel>();
        }

        public string? SearchTerm { get; set; }
        public int VehiclesPerPage { get; set; } = 6;
        public int VehicleCount { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int FirstPage { get; set; } = 1;
        public int MaxPage { get; set; }
        public string? SelectedVehicleTypes { get; set; }
        [Display(Name = "Sorting")]
        public VehicleSorting VehicleSorting { get; set; }
        public IEnumerable<VehicleSorting> SortingOptions { get; set; }
        public IEnumerable<int> PreviousPages { get; set; }
        public IEnumerable<int> NextPages { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
        public IEnumerable<VehicleListModel> Vehicles { get; set; }
    }
}
