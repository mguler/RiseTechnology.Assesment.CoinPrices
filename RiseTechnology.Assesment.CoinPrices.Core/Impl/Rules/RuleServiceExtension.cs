using Microsoft.Extensions.DependencyInjection;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    /// <summary>
    /// Rule service registration extensions
    /// </summary>
    public static class RuleServiceExtension
    {
        /// <summary>
        /// This extension is a helper method that registers rule service with pre-defined rule configurations 
        /// </summary>
        /// <param name="services">Instance of IServiceCollection</param>
        /// <param name="configurationCallback">A callback for setup rule configurations</param>
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
