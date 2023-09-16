
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Model.CoinManagement;
using System.Collections.Generic;

namespace RiseTechnology.Assesment.CoinPrices.Business.CoinManagement
{
    public class CoinManagementService : ICoinManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;

        private readonly Dictionary<string, ICoinManagementService> _coinManagementSpecificServices;
        public CoinManagementService(Dictionary<string, ICoinManagementService> coinManagementSpecificServices)
        {
            _coinManagementSpecificServices = coinManagementSpecificServices;
        }
        public GetPriceInfoResultDto GetPriceInfo(PriceInfoFilter getPriceInfoFilterDto)
        {
            //We are using multiple implementations of the service to handle open/closed principle
            var service = _coinManagementSpecificServices[getPriceInfoFilterDto.ToString()];
            var result = service.GetPriceInfo(getPriceInfoFilterDto);
            return result;
        }
    }
}