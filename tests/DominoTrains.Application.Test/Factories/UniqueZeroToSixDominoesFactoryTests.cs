using DominoTrains.Application.Factories;

namespace DominoTrains.Application.Test.Factories;

public class UniqueZeroToSixDominoesFactoryTests
{
    [Fact]
    public void CreateDominoes_CreatesExpectedList()
    {
        var dominoes = new UniqueZeroToSixDominoesFactory().CreateDominoes();

        var first = dominoes.First();
        var last = dominoes.Last();
        Assert.Equal(28, dominoes.Count);
        Assert.Equal(0, first.A);
        Assert.Equal(0, first.B);
        Assert.Equal(6, last.A);
        Assert.Equal(6, last.B);
    }
}