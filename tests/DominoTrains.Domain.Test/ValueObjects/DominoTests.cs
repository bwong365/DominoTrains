using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Test.ValueObjects;

public class DominoTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(3, 6)]
    [InlineData(6, 1)]
    public void CanBeCreated_With_ValidArguments(int a, int b)
    {
        var domino = new Domino(a, b);

        Assert.Equal(a, domino.A);
        Assert.Equal(b, domino.B);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(100, 8)]
    [InlineData(3, 7)]
    public void CannotBeCreated_With_InvalidArguments(int a, int b)
    {
        var creationAttempt = () => new Domino(a, b);

        Assert.Throws<ArgumentOutOfRangeException>(() => creationAttempt());
    }
}