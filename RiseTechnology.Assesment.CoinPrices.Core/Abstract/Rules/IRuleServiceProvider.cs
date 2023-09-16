namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules
{
    /// <summary>
    /// This abstraction is a contract for implement a service that supplies object to object mapping operations
    /// </summary>
    public interface IRuleServiceProvider
    {
        /// <summary>
        /// This method registers mapping configuration for the types
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument that contains data and will be passed into the rule definition for evaluate</typeparam>
        /// <param name="ruleName">Name of the rule</param>
        /// <param name="conf">A dynamic function that apply the rules</param>
        void Register<TArgument>(string ruleName, Func<TArgument, IRuleServiceResult> conf) where TArgument : class;
        /// <summary>
        /// This method apply rules
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument to be evaluated</typeparam>
        /// <param name="ruleName">Name of the rule</param>
        /// <param name="arg">The object that contains data to be evaluated</param>
        /// <returns>Evaluation result</returns>
        IRuleServiceResult Apply<TArgument>(string ruleName, TArgument arg) where TArgument : class;
    }
}
