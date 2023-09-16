namespace RiseTechnology.Assesment.CoinPrices.Dto.UserManagement
{
    public record RegisterDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
