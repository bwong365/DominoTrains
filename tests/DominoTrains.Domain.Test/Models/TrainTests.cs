using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Test.Models;

public class TrainTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(1)]
    public void CanBeCreated_With_ValidArguments(int initialEdgeValue)
    {
        var train = new Train(initialEdgeValue);

        Assert.Equal(initialEdgeValue, train.EdgeValue);
        Assert.Empty(train.Dominoes);
    }

    [Theory]
    [InlineData(Domino.MaxValue + 1)]
    [InlineData(Domino.MinValue - 1)]
    public void CannotBeCreated_With_InvalidArguments(int initialEdgeValue)
    {
        var creationAttempt = () => new Train(initialEdgeValue);

        Assert.Throws<ArgumentOutOfRangeException>(() => creationAttempt());
    }

    [Fact]
    public void AddDomino_ExtendsTrain()
    {
        var train = new Train(edgeValue: 6);
        var domino = new Domino(6, 4);

        train.AddDomino(domino);

        Assert.Equal(4, train.EdgeValue);
        Assert.Equal(new Domino(6, 4), train.Dominoes.First());
    }

    [Fact]
    public void AddDomino_ExtendsTrain_WhenReversed()
    {
        var train = new Train(edgeValue: 4);
        var domino = new Domino(6, 4);

        train.AddDomino(domino);

        Assert.Equal(6, train.EdgeValue);
        Assert.Equal(new Domino(4, 6), train.Dominoes.First());
    }

    [Fact]
    public void AddDomino_Fails_WhenNoMatch()
    {
        var train = new Train(edgeValue: 6);
        var domino = new Domino(0, 0);

        var addAttempt = () => train.AddDomino(domino);

        Assert.Throws<InvalidOperationException>(() => addAttempt());
    }
}