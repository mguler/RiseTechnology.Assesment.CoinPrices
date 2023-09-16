namespace RiseTechnology.Assesment.CoinPrices.Data.Dto.CoinManagement
{
    public record GetPriceInfoResultDto
    {
        public List<CoinPriceInfoDto> Prices { get; set; }
        public List<string> Labels { get; set; }
    }
}
