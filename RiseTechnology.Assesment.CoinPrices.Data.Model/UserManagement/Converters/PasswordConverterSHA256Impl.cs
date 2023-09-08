using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;
using System.Text;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement.Converters
{
    public class PasswordConverterSHA256Impl : ValueConverter<string, string>
    {
        public PasswordConverterSHA256Impl()
            : base(
                value => Hash(value),
                value => value)
        {

        }

        public static string Hash(string value)
        {
            using var sha256Hash = SHA256.Create();
            var password = Convert.ToBase64String(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value)));
            return password;
        }
    }
}
