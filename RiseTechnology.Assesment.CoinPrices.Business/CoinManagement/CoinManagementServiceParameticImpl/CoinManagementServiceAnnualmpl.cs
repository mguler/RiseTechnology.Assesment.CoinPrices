using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement.CoinManagementServiceParameticImpl
{
    /// <summary>
    /// Annual implementation of ICoinManagementService
    /// </summary>
    public class CoinManagementServiceAnnualImpl : ICoinManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        public CoinManagementServiceAnnualImpl(IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider)
        {
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
        }
        /// <summary>
        /// Returns annual coin price information
        /// </summary>
        /// <param name="getPriceInfoFilterDto">The period that the prices belong</param>
        /// <returns>Annual price information of last year</returns>
        public GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            // !!!! Timeoffset data stored as an utc date time value in the database !!!! // 
            var localStartDate = DateTimeOffset.Now.AddDays(-365);
            var utcEndDate = DateTimeOffset.UtcNow;
            var utcStartDate = utcEndDate.AddDays(-365);
            var utcDateFilter = utcStartDate.ToUnixTimeSeconds();

            var prices = _dataRepository.Get<CoinPriceHistory>().Where(price => price.Timestamp >= utcDateFilter)
                .GroupBy(price => price.Timestamp / 86400)
                .Select(g => new CoinPriceHistory
                {
                    Price = g.OrderBy(coin => coin.Timestamp).Last().Price,
                    Timestamp = g.Min(coin => coin.Timestamp)
                }).ToList();

            var ordered = Enumerable
                 .Range(0, 366)
                 .Select(n => prices.SingleOrDefault(price => price.Timestamp / 86400 == n + utcDateFilter / 86400)).ToList();

            var dtoMappedPrices = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(ordered);

            var labels = Enumerable
                .Range(0, 366)
                .Select(n => n % 30 == 0 ? utcStartDate.AddDays(n).ToString("yyyy-MMM") : "").ToList();

            var result = new GetPriceInfoResultDto
            {
                Prices = dtoMappedPrices,
                Labels = labels
            };
            return result;
        }
    }
}
