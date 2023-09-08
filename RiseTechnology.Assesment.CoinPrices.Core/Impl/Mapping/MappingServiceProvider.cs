using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;
using RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping;

namespace RiseTechnology.Assesment.CryptoTrader.Core.Impl.Mapping
{
    public class MappingServiceProvider : IMappingServiceProvider
    {
        private readonly List<MapperInfo> _cache =  new List<MapperInfo>();
        public void Register<TSource, TTarget>(Func<TSource, TTarget> mapper) where TTarget : new()
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var existis = _cache.Any(mapper => mapper.Source == sourceType && mapper.Target == targetType);
            if (existis)
            {
                throw new Exception($"a mapper already configured for the types {nameof(sourceType)} to {nameof(targetType)}");
            }

            var mappingConfiguration = new MapperInfo{ Mapper = mapper, Target = targetType, Source = sourceType };
            _cache.Add(mappingConfiguration);
        }
        public TTarget Map<TTarget>(object source) where TTarget : new()
        {
            var sourceType = source.GetType();
            var targetType = typeof(TTarget);
            var mappingConfiguration = _cache.SingleOrDefault(mapper => mapper.Source == sourceType && mapper.Target == targetType);

            if (mappingConfiguration is null)
            {
                throw new Exception($"There is no configuration registered for the types {nameof(sourceType)} to {nameof(targetType)}");
            }

            var result = mappingConfiguration.Mapper.DynamicInvoke(source);
            return (TTarget)result;
        }
    }
}
