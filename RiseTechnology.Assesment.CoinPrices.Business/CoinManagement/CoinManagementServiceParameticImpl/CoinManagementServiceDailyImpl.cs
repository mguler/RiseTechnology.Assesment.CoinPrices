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
            // !!!! Timeoffset data stored as an utc date time value in the database !!!! // 
            var localStartDate = DateTimeOffset.Now.AddHours(-24);
            var utcEndDate = DateTimeOffset.UtcNow;
            var utcStartDate = utcEndDate.AddHours(-24);
            var utcDateFilter = utcStartDate.ToUnixTimeSeconds();

            var prices = _dataRepository.Get<CoinPriceHistory>().Where(price => price.Timestamp >= utcDateFilter)
                .GroupBy(price => price.Timestamp / 3600)
                .Select(g => new CoinPriceHistory
                {
                    Price = g.OrderBy(coin => coin.Timestamp).Last().Price,
                    Timestamp = g.Min(coin => coin.Timestamp)
                }).ToList();

            var ordered = Enumerable
                 .Range(0, 25)
                 .Select(n => prices.SingleOrDefault(price => price.Timestamp / 3600 == n + utcDateFilter / 3600)).ToList();

            var dtoMappedPrices = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(ordered);

            var labels = Enumerable
                .Range(0, 25)
                .Select(n => localStartDate.AddHours(n)).Select(item => item.ToString("HH:00")).ToList();

            var result = new GetPriceInfoResultDto
            {
                Prices = dtoMappedPrices,
                Labels = labels
            };
            return result;
        }
    }
}
