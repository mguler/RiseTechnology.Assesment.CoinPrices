using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Integration;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace RiseTechnology.Assesment.CoinPrices.Integrations.BinanceImpl
{
    public class BinanceDataIntegrationProvider : IDataIntegrationProvider
    {
        public async Task<List<T>> GetAsync<T>(Dictionary<string, string> additionalArguments = null)
        {
            using var client = new HttpClient();
            var response = await client.GetFromJsonAsync<List<T>>("https://api.binance.com/api/v3/ticker/price");
            return response;
        }
    }
}