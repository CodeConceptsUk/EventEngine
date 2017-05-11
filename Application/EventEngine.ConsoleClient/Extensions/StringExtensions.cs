using System.Collections.Generic;
using System.Text;

namespace Program.Extensions
{
    public static class StringExtensions
    {
        // http://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        public static IEnumerable<string> ParseArguments(this string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                yield break;
            var sb = new StringBuilder();
            bool inQuote = false;
            foreach (char c in commandLine)
            {
                if (c == '"' && !inQuote)
                {
                    inQuote = true;
                    continue;
                }
                if (c != '"' && !(char.IsWhiteSpace(c) && !inQuote))
                {
                    sb.Append(c);
                    continue;
                }
                if (sb.Length > 0)
                {
                    var result = sb.ToString();
                    sb.Clear();
                    inQuote = false;
                    yield return result;
                }
            }
            if (sb.Length > 0)
                yield return sb.ToString();
        }

        public static string ToFixedWidth(this string value, int length)
        {
            var valueString = value ?? string.Empty;
            if (valueString.Length > length)
                return valueString.Substring(0, length);

            var newValue = $"{valueString}{new string(' ', length - valueString.Length)}";
            return newValue;
        }
    }
}