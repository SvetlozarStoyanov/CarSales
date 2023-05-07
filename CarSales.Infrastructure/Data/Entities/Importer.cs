using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Infrastructure.Data.Entities
{
    public class Importer
    {
        public Importer()
        {
            Vehicles = new HashSet<Vehicle>();
        }
        public int Id { get; set; }
        public ImporterRating ImporterRating { get; set; }
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
