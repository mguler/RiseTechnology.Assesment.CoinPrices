using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    /// <summary>
    /// Options for rule service that helps register configuration and setup rule service easly 
    /// </summary>
    public class RuleServiceExtensionOptions
    {
        internal List<Type> Types { get; } = new List<Type>();
        /// <summary>
        /// This method adds a rule configuration to options
        /// </summary>
        /// <typeparam name="T">Type of the rule configuration</typeparam>
        /// <returns>Current instance of options</returns>
        public RuleServiceExtensionOptions Add<T>() where T : IRuleServiceConfiguration
        {
            Types.Add(typeof(T));
            return this;
        }
    }
}
