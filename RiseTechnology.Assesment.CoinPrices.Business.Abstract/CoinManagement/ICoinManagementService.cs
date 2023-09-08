using RiseTechnology.Assesment.CoinPrices.Data.Dto;

namespace RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement
{
    public interface ICoinManagementService
    {
        void SavePriceInfo(List<CoinPriceInfoDto> coinPriceInfoDto);
        List<CoinPriceInfoDto> GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto);
    }
}