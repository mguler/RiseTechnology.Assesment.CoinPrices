using System.Text.RegularExpressions;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Extensions
{
    public static class StringExtensions
    {
        public static string Match(this string value, string pattern) => Regex.Match(value, pattern)?.Value;
        public static string[] Matches(this string value, string pattern) => Regex.Matches(value, pattern).Select(m=>m.Value).ToArray();
        public static bool IsMatch(this string value, string pattern) => Regex.IsMatch(value, pattern);
    }
}
