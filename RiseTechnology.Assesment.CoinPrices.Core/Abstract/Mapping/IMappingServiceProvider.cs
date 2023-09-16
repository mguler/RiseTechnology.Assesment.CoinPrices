namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping
{
    /// <summary>
    /// This abstraction is a contract for implement a service that supplies object to object mapping operations
    /// </summary>
    public interface IMappingServiceProvider
    {
        /// <summary>
        /// This method registers mapping configuration for the types
        /// </summary>
        /// <typeparam name="TSource">A clr type for source object</typeparam>
        /// <typeparam name="TTarget">A clr type for target object</typeparam>
        /// <param name="mapper">A delegate function that does mapping operation</param>
        void Register<TSource, TTarget>(Func<TSource, TTarget> mapper) where TTarget : new();
        /// <summary>
        /// This method supplies mapping functionality between two objects 
        /// </summary>
        /// <typeparam name="TTarget">A clr type for target</typeparam>
        /// <param name="source">An object that contains data</param>
        /// <returns>An object , instance of the TTarget</returns>
        TTarget Map<TTarget>(object source) where TTarget : new();
    }
}
