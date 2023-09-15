using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using System;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    public class RuleServiceProvider : IRuleServiceProvider
    {
        private readonly Dictionary<string, Delegate> _cache = new Dictionary<string, Delegate>();
        public void Register<TArgument>(string ruleName, Func<TArgument, IRuleServiceResult> conf) where TArgument : class 
        {
            if (_cache.ContainsKey(ruleName))
            {
                throw new Exception($"a ruleset already configured with the key {ruleName}");
            }
            _cache.Add(ruleName, conf);
        }

        public IRuleServiceResult Apply<TArgument>(string ruleName, TArgument arg) where TArgument : class
        {
            Delegate? rule;
            var isRuleExists = _cache.TryGetValue(ruleName, out rule);
            if (!isRuleExists)
            {
                throw new Exception($"There is no ruleset registered with the name {ruleName}");
            }
            var result = rule.DynamicInvoke(arg);
            return (IRuleServiceResult)result;
        }
    }
}
