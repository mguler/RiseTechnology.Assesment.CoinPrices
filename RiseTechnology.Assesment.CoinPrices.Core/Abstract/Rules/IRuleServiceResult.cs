namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Rules
{
    public interface IRuleServiceResult
    {
        public bool IsSuccessful { get; }
        public Dictionary<string, string> Messages { get; set; }
    }
}
