namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Integration
{
    public interface IDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="additionalArguments">Contains arguments which are specific to the current provider</param>
        /// <returns></returns>
        T Get<T>(Dictionary<string, string> additionalArguments);
    }
}
