namespace RiseTechnology.Assesment.CoinPrices.Dto.UserManagement
{
    public record LoginResultDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
