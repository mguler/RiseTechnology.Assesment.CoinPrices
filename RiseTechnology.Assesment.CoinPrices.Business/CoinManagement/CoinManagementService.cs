
using Microsoft.EntityFrameworkCore;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement
{
    public class CoinManagementService : ICoinManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        public CoinManagementService(IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider)
        {
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
        }
        public List<CoinPriceInfoDto> GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            var dateCondition = DateTime.Now.AddDays(-(int)getPriceInfoFilterDto);

            var priceInfo = _dataRepository.Get<CoinPriceHistory>()
                .Where(priceInfo => priceInfo.Timestamp >= dateCondition)
                .GroupBy(priceInfo => getPriceInfoFilterDto == PriceInfoFilter.Today ? priceInfo.Timestamp.Hour
                : getPriceInfoFilterDto == PriceInfoFilter.Month ? priceInfo.Timestamp.Day
                : priceInfo.Timestamp.Month).Select(g => new CoinPriceHistory
                {
                    Timestamp = g.Max(x => x.Timestamp),
                    Price = g.Max(x => x.Price),
                    Symbol = g.Max(x => x.Symbol)
                }).OrderBy(priceInfo => priceInfo.Timestamp).ToList(); 

            var result = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(priceInfo);

            return result;
        }
    }
}