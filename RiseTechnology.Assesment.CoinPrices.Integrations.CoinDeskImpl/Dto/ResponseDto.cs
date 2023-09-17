namespace RiseTechnology.Assesment.CoinPrices.Integrations.CoinDeskImpl.Dto
{
    public record ResponseDto
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public BodyDto data { get; set; }
    }
}
