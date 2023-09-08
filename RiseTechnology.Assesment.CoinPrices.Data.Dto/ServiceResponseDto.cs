namespace RiseTechnology.Assesment.CoinPrices.Data.Dto
{
    public class ServiceResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
    public class ServiceResponseDto<T> :ServiceResponseDto
    {
        public T Data { get; set; }
    }
}
