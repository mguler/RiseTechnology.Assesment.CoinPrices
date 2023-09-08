namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Mapping
{
    internal class MapperInfo
    {
        public required Type Source { get; internal set; }

        public required Type Target { get; internal set; }
        public required Delegate Mapper { get; internal set; } 
        
    }
}
