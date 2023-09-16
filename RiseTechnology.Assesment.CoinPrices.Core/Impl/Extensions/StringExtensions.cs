using System.Text.RegularExpressions;

namespace RiseTechnology.Assesment.CoinPrices.Core.Impl.Extensions
{
    /// <summary>
    /// Regular expression extensions for string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// This method returns a string that matches the given regex template within the provided string.
        /// </summary>
        /// <param name="value">The string to be searched within</param>
        /// <param name="pattern">a regular expression pattern</param>
        /// <returns>The string that matches the given regex template within the provided string.</returns>
        public static string Match(this string value, string pattern) => Regex.Match(value, pattern)?.Value;
        /// <summary>
        /// This method returns the strings matching the given regex template within the provided string.
        /// </summary>
        /// <param name="value">The string to be searched within</param>
        /// <param name="pattern">a regular expression pattern</param>
        /// <returns>An array containing strings that match the given regex template within the provided string.</returns>
        public static string[] Matches(this string value, string pattern) => Regex.Matches(value, pattern).Select(m=>m.Value).ToArray();
        /// <summary>
        /// This method returns whether the given string contains a substring that matches the template.
        /// </summary>
        /// <param name="value">The string to be searched within</param>
        /// <param name="pattern">a regular expression pattern</param>
        /// <returns>This method returns true if there is a substring matching the template within the given string, otherwise it returns false</returns>
        public static bool IsMatch(this string value, string pattern) => Regex.IsMatch(value, pattern);
    }
}
