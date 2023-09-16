namespace RiseTechnology.Assesment.CoinPrices.Data.Dto
{
    public record CoinPriceInfoDto
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public long Timestamp { get; set; }
    }
}
