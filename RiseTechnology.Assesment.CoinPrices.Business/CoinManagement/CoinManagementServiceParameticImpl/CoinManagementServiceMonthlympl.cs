using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement.CoinManagementServiceParameticImpl
{
    /// <summary>
    /// Monthly implementation of ICoinManagementService
    /// </summary>
    public class CoinManagementServiceMonthlyImpl : ICoinManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        public CoinManagementServiceMonthlyImpl(IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider)
        {
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
        }
        /// <summary>
        /// Returns monthly coin price information
        /// </summary>
        /// <param name="getPriceInfoFilterDto">The period that the prices belong</param>
        /// <returns>Monthly price information of last month</returns>
        public GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            var endDate = DateTimeOffset.Now;
            var startDate = endDate.AddDays(-30);
            var dateFilter = startDate.ToUnixTimeSeconds();

            var prices = _dataRepository.Get<CoinPriceHistory>().Where(price => price.Timestamp >= dateFilter).GroupBy(price => price.Timestamp / 86400)
                .Select(g => new CoinPriceHistory
                {
                    Price = g.Max(coin => coin.Price),
                    Timestamp = g.Min(coin => coin.Timestamp)
                }).ToList();

            var pricesResult = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(prices);
            var labels = Enumerable
                .Range(startDate.DayOfYear, endDate.Subtract(startDate).Days + 1)
                .Select(n => startDate.AddDays(n - startDate.DayOfYear)).Select(item => item.ToString("MMM-dd")).ToList();

            var result = new GetPriceInfoResultDto
            {
                Prices = pricesResult,
                Labels = labels
            };
            return result;
        }
    }
}
