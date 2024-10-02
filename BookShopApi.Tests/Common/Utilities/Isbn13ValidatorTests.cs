using BookShopApi.Common.Utilities;

using Xunit;

namespace BookShopApi.Tests.Common.Utilities;

public class Isbn13ValidatorTests
{
    [Theory]
    [InlineData("978-6-8787-1772-1")]
    [InlineData("978-1-5749-8131-5")]
    [InlineData("978-3-2599-8184-9")]
    public void Validate_ValidIsbn_ShouldReturnTrue(string input)
    {
        // Arrange

        // Act
        bool actualResult = Isbn13Validator.Validate(input);

        // Assert
        Assert.True(actualResult);
    }

    [Theory]
    [InlineData("978-3-2599-8184-1")]
    [InlineData("978-3-2599-8184-2")]
    [InlineData("978-3-2599-8184-3")]
    public void Validate_InvalidIsbn_ShouldReturnFalse(string input)
    {
        // Arrange

        // Act
        bool actualResult = Isbn13Validator.Validate(input);

        // Assert
        Assert.False(actualResult);
    }

    [Theory]
    [InlineData("", typeof(ArgumentException))]
    [InlineData(null, typeof(ArgumentException))]
    [InlineData("-978-3-2599-8184-9", typeof(ArgumentException))]
    [InlineData("0", typeof(ArgumentException))]
    public void Validate_BadIsbn_ShouldThrowException(string input, Type expectedException)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws(expectedException, () => Isbn13Validator.Validate(input));
    }
}
