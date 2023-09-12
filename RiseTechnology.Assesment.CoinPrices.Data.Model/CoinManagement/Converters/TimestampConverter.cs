using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RiseTechnology.Assesment.CoinPrices.Data.Model.UserManagement.Converters
{
    public class TimestampConverter : ValueConverter<DateTime, long>
    {
        public TimestampConverter()
            : base(
                value => new DateTimeOffset(value).ToUnixTimeSeconds(),
                value => DateTimeOffset.FromUnixTimeSeconds(value).Date)
        {
            
        }         
    }
}
