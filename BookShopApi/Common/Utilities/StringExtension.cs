namespace BookShopApi.Common.Utilities;

public static class StringExtensions
{
    public static int[] ExtractDigitsToArray(this string str)
    {
        ValidateInput(str);

        var digits = new int[str.Length];
        for (var i = 0; i < str.Length; i++)
        {
            digits[i] = int.Parse(str[i].ToString());
        }

        return digits;
    }

    public static bool IsNumber(this string str)
    {
        return int.TryParse(str, out int _);
    }

    private static void ValidateInput(string str)
    {
        if (str == null)
        {
            throw new ArgumentNullException(nameof(str));
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
}
