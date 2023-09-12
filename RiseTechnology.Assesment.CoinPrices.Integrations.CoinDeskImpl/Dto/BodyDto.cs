namespace RiseTechnology.Assesment.CoinPrices.Integrations.CoinDeskImpl.Dto
{
    public class BodyDto
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string ingestionStart { get; set; }
        public string interval { get; set; }
        public string src { get; set; }
        public decimal[][] entries { get; set; }
    }
}
