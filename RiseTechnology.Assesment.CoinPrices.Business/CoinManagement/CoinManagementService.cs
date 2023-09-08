
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
        public void SavePriceInfo(List<CoinPriceInfoDto> coinPriceInfoDto)
        {
            var coinPriceHistory = _mappingerviceProvider.Map<List<CoinPriceHistory>>(coinPriceInfoDto);
            _dataRepository.SaveAll(coinPriceHistory);
        }

        public List<CoinPriceInfoDto> GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            var dateCondition = new DateTime(DateTime.Now.Year
                , getPriceInfoFilterDto == PriceInfoFilter.LastYear ? 1 : DateTime.Now.Month
                , getPriceInfoFilterDto == PriceInfoFilter.LastMonth ? 1 : DateTime.Now.Day
                , 0, 0, 0);

            var priceInfo = _dataRepository.Get<CoinPriceHistory>()
                .Where(priceInfo => priceInfo.Timestamp >= dateCondition).ToList();

            var result = _mappingerviceProvider.Map<List<CoinPriceInfoDto>>(priceInfo);

            return result;
        }
    }
}