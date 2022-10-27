namespace DominoTrains.Api.Utils;

public static class StringExtensions
{
    public static string FromPascalToCamelCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return str.ToLowerInvariant();
        }

        return char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}