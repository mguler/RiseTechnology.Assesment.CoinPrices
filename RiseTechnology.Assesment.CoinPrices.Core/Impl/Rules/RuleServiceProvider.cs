using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules;
using System;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Rules
{
    /// <summary>
    /// Default implementation of IRuleServiceProvider. Responsible to provide rule service
    /// </summary>
    public class RuleServiceProvider : IRuleServiceProvider
    {
        private readonly Dictionary<string, Delegate> _cache = new Dictionary<string, Delegate>();

        /// <summary>
        /// This method registers the function that apply the rules and evaluate the given argument 
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument</typeparam>
        /// <param name="ruleName">Name of the rule</param>
        /// <param name="conf">The function that apply the rules</param>
        /// <exception cref="Exception">Throws an exception, If you attempt to register another (or the same) function with the same rule name</exception>
        public void Register<TArgument>(string ruleName, Func<TArgument, IRuleServiceResult> conf) where TArgument : class 
        {
            if (_cache.ContainsKey(ruleName))
            {
                throw new Exception($"a ruleset already configured with the key {ruleName}");
            }
            _cache.Add(ruleName, conf);
        }
        /// <summary>
        /// This method apply rules and evaluating the given argument
        /// </summary>
        /// <typeparam name="TArgument">Type of input argument</typeparam>
        /// <param name="ruleName">Name of the rule to be applied</param>
        /// <param name="arg">Input argument to be evaluated</param>
        /// <returns>An istance of IRuleServiceResult that contains result of the rule</returns>
        /// <exception cref="Exception">Throws an exception if the rule has not been registered with the given rule name</exception>
        public IRuleServiceResult Apply<TArgument>(string ruleName, TArgument arg) where TArgument : class
        {
            Delegate? rule;
            var isRuleExists = _cache.TryGetValue(ruleName, out rule);
            if (!isRuleExists)
            {
                throw new Exception($"There is no ruleset registered with the key {ruleName}");
            }
            var result = rule.DynamicInvoke(arg);
            return (IRuleServiceResult)result;
        }
    }
}
