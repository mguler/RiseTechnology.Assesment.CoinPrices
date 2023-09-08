using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Extensions;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using System.Runtime.CompilerServices;

namespace RiseTechnology.Assesment.CoinPrices.Rules.Configurations.UserManagement
{
    public class LoginRuleConfiguration : IRuleServiceConfiguration
    {
        public void Configure(IRuleServiceProvider ruleService)
        {
            ruleService.Register<LoginDto>("Login", loginDto =>
            {
                var result = new RuleServiceResult();

                if (loginDto is null || string.IsNullOrEmpty(loginDto .Username) || string.IsNullOrEmpty(loginDto.Password) 
                || !loginDto.Username.IsMatch("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$") 
                || !loginDto.Password.IsMatch("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$!%^&*()_+])[A-Za-z\\d@#$!%^&*()_+]{8,16}$"))
                {
                    result.Messages.Add("Login", "Gecersiz Kullanici yada Sifre");
                }
                return result;
            });
        }
    }
}