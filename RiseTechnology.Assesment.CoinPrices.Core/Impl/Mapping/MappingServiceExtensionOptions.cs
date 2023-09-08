using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping
{
    public class MappingServiceExtensionOptions
    {
        internal List<Type> Types { get; } = new List<Type>();
        public MappingServiceExtensionOptions Add<T>() where T : IMappingConfiguration
        {
            Types.Add(typeof(T));
            return this;
        }
    }
}
