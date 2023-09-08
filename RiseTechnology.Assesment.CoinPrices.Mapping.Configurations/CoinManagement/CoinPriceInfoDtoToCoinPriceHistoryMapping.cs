using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement
{
    public class CoinPriceInfoDtoToCoinPriceHistoryMapping : IMappingConfiguration
    {
        public void Configure(IMappingServiceProvider mappingervice)
        {
            mappingervice.Register<CoinPriceInfoDto, CoinPriceHistory>((source) =>
            {
                var result = new CoinPriceHistory();
                result.Price = source.Price;
                result.Symbol = source.Symbol;
                return result;
            });

            mappingervice.Register<List<CoinPriceInfoDto>, List<CoinPriceHistory>>((source) =>
            {
                var result = new List<CoinPriceHistory>();
                for (var index = 0; index < source.Count; index++)
                {

                    var item = new CoinPriceHistory();
                    item.Price = source[index].Price;
                    item.Symbol = source[index].Symbol;
                    result.Add(item);
                }
                return result;
            });
        }
    }
}
