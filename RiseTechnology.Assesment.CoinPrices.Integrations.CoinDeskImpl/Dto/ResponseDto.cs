namespace RiseTechnology.Assesment.CoinPrices.Integrations.CoinDeskImpl.Dto
{
    public class ResponseDto
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public BodyDto data { get; set; }
    }
}
