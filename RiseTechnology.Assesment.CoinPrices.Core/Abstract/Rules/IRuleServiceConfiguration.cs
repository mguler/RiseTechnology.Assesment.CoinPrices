namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules
{
    /// <summary>
    /// This abstraction is a contract for defining rule configurations
    /// </summary>
    public interface IRuleServiceConfiguration
    {
        /// <summary>
        /// This method supplies functionality to register a mapping configuration into a mapping service instance 
        /// </summary>
        /// <param name="ruleService">An instance of IRuleServiceProvider</param>
        public void Configure(IRuleServiceProvider ruleService);
    }
}
