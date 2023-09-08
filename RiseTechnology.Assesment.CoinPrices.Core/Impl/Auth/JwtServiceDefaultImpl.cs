using Microsoft.IdentityModel.Tokens;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Auth
{
    public class JwtServiceDefaultImpl : IJwtService
    {
        public string GenerateToke(string key, string issuer, string audience, int expirationInMinutes, Dictionary<string, string> claims)
        {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "Issuer",
                    Audience = "Audience",
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
