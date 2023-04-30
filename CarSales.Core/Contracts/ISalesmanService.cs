namespace CarSales.Core.Contracts
{
    public interface ISalesmanService
    {
        Task CreateOrRenewSalesmanAsync(string userId);

        Task RetireSalesmanAsync(string userId);
    }
}
