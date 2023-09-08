namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth
{
    public interface IJwtService
    {
        string GenerateToke(string key, string issuer, string audience, int expirationInMinutes, Dictionary<string, string> claims);
    }
}
