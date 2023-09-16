namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Mapping
{
    /// <summary>
    /// This abstraction is a contract for defining mapping configurations
    /// </summary>
    public interface IMappingConfiguration
    {
        /// <summary>
        /// This method supplies functionality to register a mapping configuration into a mapping service instance 
        /// </summary>
        /// <param name="mappingService">An instance of IMappingServiceProvider</param>
        public void Configure(IMappingServiceProvider mappingService);
    }
}
