using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement.CoinManagementServiceParameticImpl
{
    public class CoinManagementServiceAnnualImpl : ICoinManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        public CoinManagementServiceAnnualImpl(IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider)
        {
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
        }
        public GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            var endDate = DateTimeOffset.Now;
            var startDate = endDate.AddDays(-365);
            var dateFilter = startDate.ToUnixTimeSeconds();

            var prices = _dataRepository.Get<CoinPriceHistory>().Where(price => price.Timestamp >= dateFilter).GroupBy(price => price.Timestamp / 86400)
                .Select(g => new CoinPriceHistory
                {
                    Price = g.Max(coin => coin.Price),
                    Timestamp = g.Min(coin => coin.Timestamp)
                }).ToList();

            var pricesResult = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(prices);
            var monthsCount = 1+Math.Abs(12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month);

            var labels = Enumerable
                .Range(0, monthsCount)
                .Select(n => startDate.AddMonths(n)).Select(item => item.ToString("yyyy-MMM")).ToList();

            var result = new GetPriceInfoResultDto
            {
                Prices = pricesResult,
                Labels = labels
            };
            return result;
        }
    }
}
