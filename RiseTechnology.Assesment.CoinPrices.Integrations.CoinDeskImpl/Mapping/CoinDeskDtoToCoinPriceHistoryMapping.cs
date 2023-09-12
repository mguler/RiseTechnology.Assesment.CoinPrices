using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.CoinManagement
{
    public class CoinDeskDtoToCoinPriceHistoryMapping : IMappingConfiguration
    {
        public void Configure(IMappingServiceProvider mappingervice)
        {
            mappingervice.Register<decimal[][], List<CoinPriceHistory>>((source) =>
            {
                var result = new List<CoinPriceHistory>();
                for (var index = 0; index < source.Length; index++)
                {
                    var item = new CoinPriceHistory();
                    item.Timestamp = (long)source[index][0] / 1000; // -> miliseconds to seconds
                    item.Price = source[index][1];
                    item.Symbol = "BTCUSD";
                    result.Add(item);
                }
                return result;
            });
        }
    }
}
