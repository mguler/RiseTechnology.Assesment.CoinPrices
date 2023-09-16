using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping
{
    /// <summary>
    /// Options for mapping service that helps register configuration and setup mapping service easly 
    /// </summary>
    public class MappingServiceExtensionOptions
    {
        internal List<Type> Types { get; } = new List<Type>();
        /// <summary>
        /// This method ands a mapping configuration to options
        /// </summary>
        /// <typeparam name="T">Type of the mapping configuration</typeparam>
        /// <returns>Current instance of options</returns>
        public MappingServiceExtensionOptions Add<T>() where T : IMappingConfiguration
        {
            Types.Add(typeof(T));
            return this;
        }
    }
}
