using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement
{
    /// <summary>
    /// Abstraction for coin management service
    /// </summary>
    public interface ICoinManagementService
    {
        /// <summary>
        /// Returns coin price information
        /// </summary>
        /// <param name="getPriceInfoFilterDto">The peried that the prices belong</param>
        /// <returns>Price information</returns>
        GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto);
    }
}