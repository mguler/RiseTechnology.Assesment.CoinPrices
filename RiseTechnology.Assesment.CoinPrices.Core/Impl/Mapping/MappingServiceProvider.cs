using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping
{
    /// <summary>
    /// Default implementation of IMappingServiceProvider. Responsible to provide object to object mapping service
    /// </summary>
    public class MappingServiceProvider : IMappingServiceProvider
    {
        private readonly List<MapperInfo> _cache =  new List<MapperInfo>();
        /// <summary>
        /// This method registers the function that performs the mapping operation between two objects
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TTarget">The type of the target object</typeparam>
        /// <param name="mapper">Mapping function</param>
        /// <exception cref="Exception">Throws an exception, If you attempt to register another (or the same) function that performs the same mapping</exception>
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
        /// <summary>
        /// This method maps an object provided as a parameter to another object of the type specified in the Type parameter
        /// </summary>
        /// <typeparam name="TTarget">The type of the target object</typeparam>
        /// <param name="source">Source object instance</param>
        /// <returns>An object, instance of the given type in the TTarget parameter</returns>
        /// <exception cref="Exception">Throws an exception if the appropriate mapping function has not been registered before</exception>
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
