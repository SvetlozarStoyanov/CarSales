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

        [Display(Name = "Sorting")]
        public string? SearchTerm { get; set; }
        public int VehiclesPerPage { get; set; } = 6;
        public int VehiclesCount { get; set; }
        public int CurrentPage { get; set; } = 1;
        public string? SelectedVehicleTypes { get; set; }
        public VehicleSorting VehicleSorting { get; set; }
        public IEnumerable<VehicleSorting> SortingOptions { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
        //public IEnumerable<VehicleType>? SelectedVehicleTypes { get; set; }
        public IEnumerable<VehicleListModel> Vehicles { get; set; }
    }
}
