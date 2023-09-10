using RiseTechnology.Assesment.CoinPrices.Data.Dto;

namespace RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement
{
    public interface ICoinManagementService
    {
        List<CoinPriceInfoDto> GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto);
    }
}