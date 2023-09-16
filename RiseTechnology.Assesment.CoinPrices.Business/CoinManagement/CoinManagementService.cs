using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement
{
    /// <summary>
    /// Default imtlementation of ICoinManagementService
    /// </summary>
    public class CoinManagementService : ICoinManagementService
    {
        private readonly Dictionary<string, ICoinManagementService> _coinManagementSpecificServices;
        public CoinManagementService(Dictionary<string, ICoinManagementService> coinManagementSpecificServices)
        {
            _coinManagementSpecificServices = coinManagementSpecificServices;
        }
        /// <summary>
        /// Returns coin price information
        /// </summary>
        /// <param name="getPriceInfoFilterDto">The peried that the prices belong</param>
        /// <returns>Price information</returns>
        public GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            //We are using multiple implementations of the service to handle open/closed principle
            var service = _coinManagementSpecificServices[getPriceInfoFilterDto.ToString()];
            var result = service.GetPriceInfo(getPriceInfoFilterDto);
            return result;
        }
    }
}