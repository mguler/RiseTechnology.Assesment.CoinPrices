﻿using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Extensions;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Rules.Configurations.UserManagement
{
    public class RegisterRuleConfiguration : IRuleServiceConfiguration
    {
        public void Configure(IRuleServiceProvider ruleService)
        {
            ruleService.Register<RegisterDto>("Register", (registerDto) =>
            {
                var result = new RuleServiceResult();

                if (!registerDto.Firstname.IsMatch("[A-Za-z ]{2,16}")) 
                {
                    result.Messages.Add("Firstname", "Isim alani 2-16 karakter uzunlugunda harf ve bosluktan olusabilir");
                }

                if (!registerDto.Firstname.IsMatch("[A-Za-z ]{2,16}"))
                {
                    result.Messages.Add("Lastname", "Soyisim alani 2-16 karakter uzunlugunda harf ve bosluktan olusabilir");
                }

                if (!registerDto.Email.IsMatch("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$"))
                {
                    result.Messages.Add("Email", "En fazla 16 karakter uzunlugunda gecerli bir e-posta adresi girilmelidir");
                }

                if (!registerDto.Password.IsMatch("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@#$!%^&*()_+])[A-Za-z\\d@#$!%^&*()_+]{8,16}$"))
                {
                    result.Messages.Add("Password", "Sifre 8-16 karakter uzunlugunda buyuk/kucuk harf ,rakam ve @#$!%^&*()_+ karakterlerinden en az bir tanesinden olusmalidir");
                }

                if (registerDto.Password != registerDto.RePassword) 
                {
                    result.Messages.Add("RePassword", "Girilen iki sifre birbirinin ayni olmalidir");
                }

                return result;
            });
        }
    }
}