using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement.CoinManagementServiceParameticImpl
{
    /// <summary>
    /// Daily implementation of ICoinManagementService
    /// </summary>
    public class CoinManagementServiceDailyImpl : ICoinManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        public CoinManagementServiceDailyImpl(IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider)
        {
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
        }
        /// <summary>
        /// Returns daily coin price information
        /// </summary>
        /// <param name="getPriceInfoFilterDto">The period that the prices belong</param>
        /// <returns>Daily price information of last 24 hours</returns>
        public GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            var endDate = DateTimeOffset.Now;
            var startDate = endDate.AddHours(-24);
            var dateFilter = startDate.ToUnixTimeSeconds();

            var prices = _dataRepository.Get<CoinPriceHistory>().Where(price => price.Timestamp >= dateFilter).GroupBy(price => price.Timestamp / 3600)
                .Select(g => new CoinPriceHistory
                {
                    Price = g.Max(coin => coin.Price),
                    Timestamp = g.Min(coin => coin.Timestamp)
                }).ToList();

            var pricesResult = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(prices);
            
            var labels = Enumerable
                .Range(0, 25)
                .Select(n => startDate.AddHours(n)).Select(item => item.ToString("hh:00")).ToList();


            var result = new GetPriceInfoResultDto
            {
                Prices = pricesResult, 
                Labels = labels
            };
            return result;
        }
    }
}
