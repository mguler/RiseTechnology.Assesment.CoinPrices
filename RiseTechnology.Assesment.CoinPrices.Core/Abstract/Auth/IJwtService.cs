namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth
{
    /// <summary>
    /// This abstraction is a contract for implement a service class for creating a json web token
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Token generation contract
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer">Token issuer</param>
        /// <param name="audience">Token audience</param>
        /// <param name="expirationInMinutes">Token expiration in minutes</param>
        /// <param name="claims">A dictionary object that contain claims</param>
        /// <returns>a string object that contains a JWT</returns>
        string GenerateToken(string key, string issuer, string audience, int expirationInMinutes, Dictionary<string, string> claims);
    }
}
