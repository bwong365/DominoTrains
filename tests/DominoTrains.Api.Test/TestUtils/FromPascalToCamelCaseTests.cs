using DominoTrains.Api.Utils;

namespace DominoTrains.Api.Test.TestUtils;

public class FromPascalToCamelCaseTests
{
    [Theory]
    [InlineData("PascalCase", "pascalCase")]
    [InlineData("Pascal", "pascal")]
    [InlineData("P", "p")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void FromPascalToCamelCase(string input, string expected)
    {
        var result = input.FromPascalToCamelCase();
        Assert.Equal(expected, result);
    }
}