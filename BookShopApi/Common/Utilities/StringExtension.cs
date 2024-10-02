namespace BookShopApi.Common.Utilities;

public static class StringExtensions
{
    public static bool IsNumber(this string str)
    {
        return long.TryParse(str, out long _);
    }

    public static int[] ExtractDigitsToArray(this string str)
    {
        ValidateExtractDigitsToArrayInput(str);

        var digits = new int[str.Length];
        for (var i = 0; i < str.Length; i++)
        {
            digits[i] = int.Parse(str[i].ToString());
        }

        return digits;
    }

    public static string ExtractHyphens(this string str)
    {
        ValidateExtractHyphensInput(str);

        return str.Any(symbol => symbol == '-') ? str.Replace("-", string.Empty) : str;
    }

    private static void ValidateExtractDigitsToArrayInput(string str)
    {
        if (str == null)
        {
            throw new ArgumentNullException(nameof(str), string.Empty);
        }

        if (str == string.Empty)
        {
            throw new ArgumentException(string.Empty, nameof(str));
        }

        if (!str.IsNumber())
        {
            throw new ArgumentException(string.Empty, nameof(str));
        }
    }

    private static void ValidateExtractHyphensInput(string str)
    {
        if (str == null)
        {
            throw new ArgumentNullException(nameof(str), string.Empty);
        }

        if (str == string.Empty)
        {
            throw new ArgumentException(string.Empty, nameof(str));
        }
    }
}
