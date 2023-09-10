
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Mapping.Configurations.UserManagement
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
