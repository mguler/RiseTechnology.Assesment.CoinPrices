
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using RiseTechnology.Assesment.CryptoTrader.Data.Model.UserManagement;

namespace RiseTechnology.Assesment.CryptoTrader.Mapping.MappingConfigurations.CryptoManagement
{
    public class UserToUsertDtoMapping : IMappingConfiguration
    {
        public void Configure(IMappingServiceProvider mappingervice)
        {
            mappingervice.Register<User,UserDto>((source) => {
                var result = new UserDto();
                result.Fullname = $"{source.Firstname} {source.Lastname}";
                result.Email = source.Email;
                return result;
            });
        }
    }
}
