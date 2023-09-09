
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;

namespace RiseTechnology.Assesment.CryptoTrader.Mapping.MappingConfigurations.CryptoManagement
{
    public class UserToDictionaryMapping : IMappingConfiguration
    {
        public void Configure(IMappingServiceProvider mappingervice)
        {
            mappingervice.Register<User, Dictionary<string,string>>((source) => {
                var result = new Dictionary<string,string> ();
                result.Add("Fullname", $"{source.Firstname} {source.Lastname}");
                result.Add("Email", source.Email);
                return result;
            });
        }
    }
}
