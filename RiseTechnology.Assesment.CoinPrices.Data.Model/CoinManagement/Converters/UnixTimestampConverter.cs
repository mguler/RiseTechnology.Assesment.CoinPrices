using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement.Converters
{
    public class UnixTimestampConverter : ValueConverter<DateTime, long>
    {
        public UnixTimestampConverter()
            : base(
                value => new DateTimeOffset(value).ToUnixTimeSeconds(),
                value => DateTimeOffset.FromUnixTimeSeconds(value).Date)
        {
            
        }         
    }
}
