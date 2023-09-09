using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl.AdoJobStore.Common;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Integration;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Integration;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Integrations.HostApp
{
    public class QuartzNetDefaultDataIntegrationJobImpl<T>: IJob where T : IDataIntegrationProvider, new()
    {
        private readonly ILogger<T> _logger;
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingServiceProvider;
        public QuartzNetDefaultDataIntegrationJobImpl(ILogger<T> logger, IDataRepository dataRepository, IMappingServiceProvider mappingServiceProvider) 
        {
            _logger = logger;
            _dataRepository = dataRepository;
            _mappingServiceProvider = mappingServiceProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dataIntegrationProvider = new T();
                var result = await dataIntegrationProvider.GetAsync<CoinPriceInfoDto>();
                var prices = _mappingServiceProvider.Map<List<CoinPriceHistory>>(result);
                foreach (var priceInfo in prices) 
                {
                    _dataRepository.Save(priceInfo);
                }

                _logger.LogInformation("Task finished sucessfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured", ex);
            }
        }
    }
}
