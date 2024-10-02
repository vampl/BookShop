using BookShopApi.Common.Utilities;

using Xunit;
using Xunit.Abstractions;

namespace BookShopApi.Tests.Common.Utilities;

public class StringExtensionTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StringExtensionTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("1")]
    [InlineData("-14")]
    [InlineData("154")]
    public void IsNumber_StringIsNumber_ReturnsTrue(string input)
    {
        // Arrange

        // Act
        bool actualResult = input.IsNumber();

        // Assert
        Assert.True(actualResult);
    }

    [Theory]
    [InlineData("text")]
    [InlineData(null)]
    [InlineData("")]
    public void IsNumber_StringIsNotANumber_ReturnsFalse(string input)
    {
        // Arrange

        // Act
        bool actualResult = input.IsNumber();

        // Assert
        Assert.False(actualResult);
    }


    [Theory]
    [InlineData("1234", new[] { 1, 2, 3, 4 })]
    [InlineData("5", new[] { 5 })]
    [InlineData("67890", new[] { 6, 7, 8, 9, 0 })]
    public void ExtractDigitsToArray_ValidString_ShouldReturnCorrectDigits(string input, int[] expectedResult)
    {
        // Arrange


        // Act
        int[] actualResult = input.ExtractDigitsToArray();

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("dummy text", typeof(ArgumentException))]
    [InlineData(null, typeof(ArgumentNullException))]
    [InlineData("", typeof(ArgumentException))]
    public void ExtractDigitsArray_InvalidString_ShouldReturnNull(string input, Type expectedException)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws(expectedException, input.ExtractDigitsToArray);
    }

    [Theory]
    [InlineData("dummy-text", "dummytext")]
    [InlineData("dummy-text-with-more-hyphens", "dummytextwithmorehyphens")]
    [InlineData("-------", "")]
    public void ExtractHyphens_StringHaveHyphens_ShouldReturnStringWithoutHyphens(string input, string expectedResult)
    {
        // Arrange


        // Act
        string actualResult = input.ExtractHyphens();
        _testOutputHelper.WriteLine(actualResult);

        // Assert
        Assert.DoesNotContain("-", actualResult);
        Assert.Equal(expectedResult.Length, actualResult.Length);
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("dummy text", "dummy text")]
    [InlineData("dummy text without hyphens", "dummy text without hyphens")]
    [InlineData("     ", "     ")]
    public void ExtractHyphens_StringHaveNotHyphens_ShouldReturnString(string input, string expectedResult)
    {
        // Arrange


        // Act
        string actualResult = input.ExtractHyphens();

        // Assert
        Assert.Equal(expectedResult.Length, actualResult.Length);
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(null, typeof(ArgumentNullException))]
    [InlineData("", typeof(ArgumentException))]
    public void ExtractHyphens_InvalidString_ShouldThrowException(string input, Type expectedException)
    {
        // Arrange

        // Act

        // Assert
        Assert.Throws(expectedException, input.ExtractDigitsToArray);
    }
}
