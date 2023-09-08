using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    public class RuleServiceResult : IRuleServiceResult
    {
        public bool IsSuccessful { get => !Messages.Any(); }
        public Dictionary<string, string> Messages { get; set; } = new Dictionary<string, string>();
    }
}
