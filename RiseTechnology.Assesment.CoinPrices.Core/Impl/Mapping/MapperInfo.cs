namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping
{
    /// <summary>
    /// Contains information about the mapping configuration
    /// </summary>
    internal class MapperInfo
    {
        /// <summary>
        /// Clr type of the source object 
        /// </summary>
        public required Type Source { get; internal set; }
        /// <summary>
        /// Clr type of the target object  
        /// </summary>
        public required Type Target { get; internal set; }
        /// <summary>
        /// Mapping function
        /// </summary>
        public required Delegate Mapper { get; internal set; }         
    }
}
