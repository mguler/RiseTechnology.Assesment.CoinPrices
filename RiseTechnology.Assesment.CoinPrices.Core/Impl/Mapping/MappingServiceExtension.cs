using Microsoft.Extensions.DependencyInjection;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping
{
    /// <summary>
    /// Mapping service registration extensions
    /// </summary>
    public static class MappingServiceExtension
    {
        /// <summary>
        /// This extension is a helper method that registers mapping service with pre-defined mapping configurations 
        /// </summary>
        /// <param name="services">Instance of IServiceCollection</param>
        /// <param name="configurationCallback">A callback for setup mapping configurations</param>
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
