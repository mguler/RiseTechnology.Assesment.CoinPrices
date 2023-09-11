using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement
{
    public interface ICoinManagementService
    {
        GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto);
    }
}