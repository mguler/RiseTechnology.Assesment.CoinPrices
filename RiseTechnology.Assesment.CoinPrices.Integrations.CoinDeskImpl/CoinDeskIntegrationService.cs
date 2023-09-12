using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Integrations.CoinDeskImpl.Dto;
using RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement;
using System.Net.Http.Json;

namespace RiseTechnology.Assesment.CoinPrices.Integrations.CoinDeskImpl
{
    public class CoinDeskIntegrationService: BackgroundService
    {
        private readonly ILogger<CoinDeskIntegrationService> _logger;
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingServiceProvider;
        public CoinDeskIntegrationService(ILogger<CoinDeskIntegrationService> logger, IDataRepository dataRepository, IMappingServiceProvider mappingServiceProvider)
        {
            _logger = logger;
            _dataRepository = dataRepository;
            _mappingServiceProvider = mappingServiceProvider;            
        }
        protected override async Task ExecuteAsync(CancellationToken cancelToken)
        {
            var mappingConfiguration = new CoinDeskDtoToCoinPriceHistoryMapping();
            mappingConfiguration.Configure(_mappingServiceProvider);

            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    using var client = new HttpClient();
                    var url = $"https://production.api.coindesk.com/v2/tb/price/values/BTC?start_date={DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-ddT00:00")}&end_date={DateTime.Now.ToString("yyyy-MM-ddT23:59")}&ohlc=false";
                    var response = await client.GetFromJsonAsync<ResponseDto>(url);

                    var prices = _mappingServiceProvider.Map<List<CoinPriceHistory>>(response.data.entries);
                    foreach (var priceInfo in prices)
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