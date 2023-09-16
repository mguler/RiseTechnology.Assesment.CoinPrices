namespace RiseTechnology.Assesment.CoinPrices.Data.Dto
{
    public record ServiceResultDto
    {
        public bool IsSuccessful { get; set; }
        public Dictionary<string, string> Message { get; set; } = new Dictionary<string, string>();
    }
    public record ServiceResultDto<T> :ServiceResultDto
    {
        public T Data { get; set; }
    }

}
