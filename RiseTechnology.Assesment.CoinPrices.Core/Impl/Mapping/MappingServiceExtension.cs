using Microsoft.Extensions.DependencyInjection;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;

namespace RiseTechnology.Assesment.CryptoTrader.Core.Impl.Mapping
{
    public static class MappingServiceExtension
    {
        public static void AddMappingService(this IServiceCollection services, Action<MappingServiceExtensionOptions> configurationCallback)
        {
            var options = new MappingServiceExtensionOptions();
            configurationCallback(options);

            services.AddSingleton<IMappingServiceProvider>((serviceProvider) =>
            {
                var mappingService = new MappingServiceProvider();
                foreach (var type in options.Types)
                {
                    var configurationInstance = type.GetConstructor(Type.EmptyTypes).Invoke(null) as IMappingConfiguration;
                    configurationInstance.Configure(mappingService);
                }
                return mappingService;
            });
        }
    }
}
