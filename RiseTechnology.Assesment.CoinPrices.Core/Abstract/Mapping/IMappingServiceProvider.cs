namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping
{
    public interface IMappingServiceProvider
    {
        void Register<TSource, TTarget>(Func<TSource, TTarget> mapper) where TTarget : new();
        TTarget Map<TTarget>(object source) where TTarget : new();
    }
}
