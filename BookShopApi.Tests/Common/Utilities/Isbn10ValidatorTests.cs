using BookShopApi.Common.Utilities;

using Xunit;

namespace BookShopApi.Tests.Common.Utilities;

public class Isbn10ValidatorTests
{
    [Theory]
    [InlineData("0-6251-2620-3")]
    [InlineData("0-3958-2014-6")]
    [InlineData("0-6346-8143-5")]
    public void Validate_ValidIsbn_ShouldReturnTrue(string input)
    {
        // Arrange

        // Act
        bool actualResult = Isbn10Validator.Validate(input);

        // Assert
        Assert.True(actualResult);
    }

    [Theory]
    [InlineData("0-6346-8143-1")]
    [InlineData("0-6346-8143-2")]
    [InlineData("0-6346-8143-3")]
    public void Validate_InvalidIsbn_ShouldReturnFalse(string input)
    {
        // Arrange

        // Act
        bool actualResult = Isbn10Validator.Validate(input);

        // Assert
        Assert.False(actualResult);
    }

    [Theory]
    [InlineData("", typeof(ArgumentException))]
    [InlineData(null, typeof(ArgumentException))]
    [InlineData("-0-6346-8143-5", typeof(ArgumentException))]
    [InlineData("0", typeof(ArgumentException))]
    public void Validate_BadIsbn_ShouldThrowException(string input, Type expectedException)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws(expectedException, () => Isbn10Validator.Validate(input));
    }
}
