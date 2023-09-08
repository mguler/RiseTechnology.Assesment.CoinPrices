using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    public class RuleServiceExtensionOptions
    {
        internal List<Type> Types { get; } = new List<Type>();
        public RuleServiceExtensionOptions Add<T>() where T : IRuleServiceConfiguration
        {
            Types.Add(typeof(T));
            return this;
        }
    }
}
