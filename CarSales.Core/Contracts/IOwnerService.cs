namespace CarSales.Core.Contracts
{
    public interface IOwnerService
    {
        Task CreateOwnerAsync(string userId);
    }
}
