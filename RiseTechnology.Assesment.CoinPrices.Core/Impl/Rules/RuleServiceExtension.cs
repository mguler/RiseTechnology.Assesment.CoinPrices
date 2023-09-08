using Microsoft.Extensions.DependencyInjection;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    public static class RuleServiceExtension
    {
        public static void AddRuleService(this IServiceCollection services, Action<RuleServiceExtensionOptions> configurationCallback)
        {
            var options = new RuleServiceExtensionOptions();
            configurationCallback(options);

            services.AddSingleton<IRuleServiceProvider>((serviceProvider) =>
            {
                var ruleService = new RuleServiceProvider();
                foreach (var type in options.Types)
                {
                    var configurationInstance = type.GetConstructor(Type.EmptyTypes).Invoke(null) as IRuleServiceConfiguration;
                    configurationInstance.Configure(ruleService);
                }
                return ruleService;
            });
        }
    }
}
