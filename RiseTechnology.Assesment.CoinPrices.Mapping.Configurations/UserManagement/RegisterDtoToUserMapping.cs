using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CryptoTrader.Mapping.MappingConfigurations.CryptoManagement
{
    public class RegisterDtoToUserMapping : IMappingConfiguration
    {
        public void Configure(IMappingServiceProvider mappingervice)
        {
            mappingervice.Register<RegisterDto, User>((source) => {
                var result = new User();
                result.Firstname = source.Firstname;
                result.Lastname = source.Lastname;
                result.Email = source.Email;
                result.Password = source.Password;
                result.IsActive = true;
                return result;
            });
        }
    }
}
