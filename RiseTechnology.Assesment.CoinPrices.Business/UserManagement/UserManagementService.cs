using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CryptoTrader.Business.UserManagement
{
    public class UserManagementService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        private readonly IRuleServiceProvider _ruleServiceProvider;
        public UserManagementService(IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider, IRuleServiceProvider ruleServiceProvider)
        {
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
            _ruleServiceProvider = ruleServiceProvider;
        }
        public void Register(RegisterDto registerDto) 
        {
            _ruleServiceProvider.Apply("Register", registerDto);
            var user = _mappingerviceProvider.Map<User>(registerDto);
            _dataRepository.Save(user);
        }
        public void Login(LoginDto loginDto) 
        {
            var result = default(UserDto);
            var user = _dataRepository.Get<User>().SingleOrDefault(user => user.Email == loginDto.Username
            && user.Password == loginDto.Password && user.IsActive);

            if (user != null) 
            {
                var mapped  = _mappingerviceProvider.Map<UserDto>(loginDto);
            }
        }
        public void Logout() 
        {
            
        }
    }
}
