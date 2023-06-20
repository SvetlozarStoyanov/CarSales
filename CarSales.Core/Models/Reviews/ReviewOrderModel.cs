using CarSales.Infrastructure.Data.Enums;

namespace CarSales.Core.Models.Reviews
{
    public class ReviewOrderModel
    {
        public ReviewOrderModel()
        {
            ReviewTypesAndPrices = new Dictionary<ReviewType, decimal>();
        }

        public int Id { get; set; }
        public ReviewType ReviewType { get; set; }
        public decimal Price { get; set; }
        public int ReviewerId { get; set; }
        public int VehicleId { get; set; }
        public IDictionary<ReviewType, decimal> ReviewTypesAndPrices { get; set; }
    }
}
