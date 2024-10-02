namespace BookShopApi.Common.Utilities;

public static class Isbn13Validator
{
    public static bool Validate(string isbn)
    {
        ValidateValidationInput(isbn);

        string preparedIsbn = isbn.ExtractHyphens();
        int[] digits = preparedIsbn.ExtractDigitsToArray();
        long sum = 0;
        for (var i = 1; i <= digits.Length; i++)
        {
            sum += digits[i - 1] * (i % 2 != 0 ? 1 : 3);
        }

        return sum % 10 == 0;
    }

    private static void ValidateValidationInput(string isbn)
    {
        if (string.IsNullOrEmpty(isbn))
        {
            throw new ArgumentException(string.Empty, nameof(isbn));
        }

        if (isbn[0] == '-')
        {
            throw new ArgumentException(string.Empty, nameof(isbn));
        }

        if (isbn.Length != 17)
        {
            throw new ArgumentException("ISBN should be 17 characters long.", nameof(isbn));
        }
    }

}
