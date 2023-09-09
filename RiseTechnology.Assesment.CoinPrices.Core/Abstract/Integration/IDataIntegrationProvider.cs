namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Integration
{
    public interface IDataIntegrationProvider
    {
        Task<List<T>> GetAsync<T>(Dictionary<string, string> additionalArguments = null);
    }
}
