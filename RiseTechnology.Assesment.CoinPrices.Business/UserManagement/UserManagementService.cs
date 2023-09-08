using RiseTechnology.Assesment.CoinPrices.Business.Abstract.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.UserManagement
{
    public class UserManagementService: IUserManagementService
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
        public ServiceResultDto Register(RegisterDto registerDto)
        {
            var result = new ServiceResultDto();
            try
            {
                var validationResult = _ruleServiceProvider.Apply("Register", registerDto);
                result.Message = validationResult.Messages;

                if (validationResult.IsSuccessful)
                {
                    var isExists = _dataRepository.Get<User>().Any(user => user.Email == registerDto.Email);

                    if (!isExists)
                    {
                        var user = _mappingerviceProvider.Map<User>(registerDto);
                        _dataRepository.Save(user);
                        result.IsSuccessful = true;
                    }
                    else 
                    {
                        result.Message.Add("Email", "Email adresi daha onceden kaydedilmis");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message.Add("Error", "Hata Meydana Geldi, Sistem Yoneticisine Basvurunuz ");
                //TODO : Implement a log and alert notification logic here
            }
            return result;
        }
        public ServiceResultDto<LoginResultDto> Login(LoginDto loginDto)
        {
            var result = new ServiceResultDto<LoginResultDto>();
            try
            {
                var validationResult = _ruleServiceProvider.Apply("Login", loginDto);
                result.Message = validationResult.Messages;
                if (validationResult.IsSuccessful)
                {
                    var user = _dataRepository.Get<User>().SingleOrDefault(user => user.Email == loginDto.Username
                       && user.Password == loginDto.Password && user.IsActive);

                    if (user != null)
                    {
                        var mapped = _mappingerviceProvider.Map<UserDto>(user);
                        result.IsSuccessful = true;
                    }
                    else 
                    {
                        result.Message.Add("Login", "Gecersiz Kullanici yada Sifre");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message.Add("Error", "Hata Meydana Geldi, Sistem Yoneticisine Basvurunuz ");
                //TODO : Implement a log and alert notification logic here
            }
            return result;
        }

        public void Logout() 
        {
            
        }
    }
}
