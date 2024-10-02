namespace BookShopApi.Common.Utilities;

public static class Isbn10Validator
{
    public static bool Validate(string isbn)
    {
        ValidateValidationInput(isbn);

        string preparedIsbn = isbn.ExtractHyphens();
        int[] digits = preparedIsbn.ExtractDigitsToArray();
        long sum = digits.Select((digit, index) => digit * (digits.Length - index)).Sum();

        return sum % 11 == 0;
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

        if (isbn.Length != 13)
        {
            throw new ArgumentException("ISBN should be 13 characters long.", nameof(isbn));
        }
    }
}
