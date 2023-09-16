using Microsoft.IdentityModel.Tokens;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Auth
{
    /// <summary>
    /// Default Implementation if the IJwtService
    /// </summary>
    public class JwtServiceDefaultImpl : IJwtService
    {
        /// <summary>
        /// This method generates a JWT using the given arguments
        /// </summary>
        /// <param name="key">A secret key for generation hash value</param>
        /// <param name="issuer">Token issuer</param>
        /// <param name="audience">Token auience</param>
        /// <param name="expirationInMinutes">Token expiration time in minutes</param>
        /// <param name="claims">Claim information</param>
        /// <returns>A string that contains the generated json web token</returns>
        public string GenerateToken(string key, string issuer, string audience, int expirationInMinutes, Dictionary<string, string> claims)
        {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new ClaimsIdentity(claims.Keys.Select(claim => new Claim(claim, claims[claim].ToString()))),
                    Expires = DateTime.Now.AddMinutes(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var result = tokenHandler.WriteToken(token);
                return result;
        }
    }
}
