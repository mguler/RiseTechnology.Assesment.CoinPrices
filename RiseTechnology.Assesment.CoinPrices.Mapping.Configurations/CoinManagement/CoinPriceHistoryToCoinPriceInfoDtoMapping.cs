using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement
{
    public class CoinPriceHistoryToCoinPriceInfoDtoMapping : IMappingConfiguration
    {
        public void Configure(IMappingServiceProvider mappingervice)
        {
            mappingervice.Register<CoinPriceHistory, CoinPriceInfoDto>((source) =>
            {
                var result = new CoinPriceInfoDto();
                result.Price = source.Price;
                result.Symbol = source.Symbol;
                result.Timestamp = source.Timestamp;
                return result;
            });

            mappingervice.Register<List<CoinPriceHistory>, List<CoinPriceInfoDto>>((source) =>
            {
                var result = new List<CoinPriceInfoDto>();
                for (var index = 0; index < source.Count; index++)
                {

                    var item = new CoinPriceInfoDto();
                    item.Price = source[index].Price;
                    item.Symbol = source[index].Symbol;
                    item.Timestamp = source[index].Timestamp;
                    result.Add(item);
                }
                return result;
            });
        }
    }
}
