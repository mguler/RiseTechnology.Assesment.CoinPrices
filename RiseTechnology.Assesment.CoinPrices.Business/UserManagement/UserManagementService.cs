
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Configuration;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.UserManagement
{
    /// <summary>
    /// Default implementation of IUserManagementService
    /// </summary>
    public class UserManagementService: IUserManagementService
    {
        private readonly IOptions<TokenOptions> _tokenOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataRepository _dataRepository;
        private readonly IMappingServiceProvider _mappingerviceProvider;
        private readonly IRuleServiceProvider _ruleServiceProvider;
        private readonly IJwtService _jwtService;
        public UserManagementService(IOptions<TokenOptions> tokenOptions, IHttpContextAccessor httpContextAccessor, IDataRepository dataRepository, IMappingServiceProvider mappingerviceProvider, IRuleServiceProvider ruleServiceProvider,IJwtService jwtService)
        {
            _tokenOptions = tokenOptions;
            _httpContextAccessor = httpContextAccessor;
            _dataRepository = dataRepository;
            _mappingerviceProvider = mappingerviceProvider;
            _ruleServiceProvider = ruleServiceProvider;
            _jwtService = jwtService;
        }

        /// <summary>
        /// This method registers a user with the given information
        /// </summary>
        /// <param name="registerDto">Contains information about the user to be registered</param>
        /// <returns>Contains information about the result of the register operation</returns>
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
        /// <summary>
        /// This method supplies login functionality 
        /// </summary>
        /// <param name="loginDto">User credentials</param>
        /// <returns>Information about the login information if the operation is successful this object contains a JWT token</returns>
        public ServiceResultDto<LoginResultDto> Login(LoginDto loginDto)
        {
            var result = new ServiceResultDto<LoginResultDto> { Data = new LoginResultDto() };
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
                        var claims = _mappingerviceProvider.Map<Dictionary<string, string>>(user);
                        result.Data.Token = _jwtService.GenerateToken(_tokenOptions.Value.Key, _tokenOptions.Value.Issuer, _tokenOptions.Value.Audience, 100, claims);
                        result.Data.User = _mappingerviceProvider.Map<UserDto>(user);
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true
                        };
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("Jwt", result.Data.Token, cookieOptions);
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
        /// <summary>
        /// This method supplies logout functionality if operation is successful related cookies will be deleted 
        /// </summary>
        /// <returns>Contains information about operation result</returns>
        public ServiceResultDto Logout() 
        {
            var result = new ServiceResultDto<LoginResultDto> { Data = new LoginResultDto() };
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("Jwt");
            return result;
        }
    }
}
