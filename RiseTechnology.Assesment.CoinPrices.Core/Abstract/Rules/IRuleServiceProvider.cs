namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules
{
    public interface IRuleServiceProvider
    {
        void Register<TArgument>(string ruleName, Func<TArgument, IRuleServiceResult> conf) where TArgument : class;
        IRuleServiceResult Apply<TArgument>(string ruleName, TArgument arg) where TArgument : class;
    }
}
