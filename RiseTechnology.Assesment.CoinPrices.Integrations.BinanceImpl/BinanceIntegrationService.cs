using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;
using System.Net.Http.Json;

namespace RiseTechnology.Assesment.CoinPrices.Integrations.BinanceImpl
{
    public class BinanceIntegrationService : BackgroundService
    {
        private readonly ILogger<BinanceIntegrationService> _logger;
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingServiceProvider;
        public BinanceIntegrationService(ILogger<BinanceIntegrationService> logger, IDataRepository dataRepository, IMappingServiceProvider mappingServiceProvider)
        {
            _logger = logger;
            _dataRepository = dataRepository;
            _mappingServiceProvider = mappingServiceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    using var client = new HttpClient();
                    var response = await client.GetFromJsonAsync<List<CoinPriceInfoDto>>("https://api.binance.com/api/v3/ticker/price");
                    var prices = _mappingServiceProvider.Map<List<CoinPriceHistory>>(response);
                    foreach(var priceInfo in prices) 
                    {
                        _dataRepository.Save(priceInfo);
                    }

                    _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");
                }

                catch (Exception ex)
                {
                    _logger.LogError($"Error ocurred at: {DateTimeOffset.Now} {ex}");
                }

                await Task.Delay(1000, cancelToken);
            }
        }
    }
}
