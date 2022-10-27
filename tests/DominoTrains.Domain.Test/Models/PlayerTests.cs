using DominoTrains.Domain.Exceptions.CustomExceptions;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Test.Models;

public class PlayerTests
{
    [Fact]
    public void CreatesCorrectly()
    {
        var dominoes = new List<Domino>
        {
            new (1, 2),
            new (3, 4),
            new (5, 6)
        };

        var player = new Player(dominoes);

        Assert.Equal(3, player.Dominoes.Count);
    }

    [Fact]
    public void GetHandValue_SumsDotsOnDominoes()
    {
        var player = new Player(new List<Domino>
        {
             new (0, 0),
             new (2, 4),
             new (5, 6)
        });

        Assert.Equal(17, player.GetDotsInHand());
    }

    [Fact]
    public void PlayDomino_MovesDominoFromPlayerToTrain()
    {
        var player = new Player(new List<Domino>
        {
            new (1, 5),
        });

        var train = new Train(1);
        player.PlayDomino(0, train);

        Assert.Empty(player.Dominoes);
        Assert.Equal(1, train.Dominoes.Count);
        Assert.Equal(5, train.EdgeValue);
    }

    [Fact]
    public void PlayDomino_ThrowsException_WhenTrainCannotPlayDomino()
    {
        var player = new Player(new List<Domino>
        {
            new (1, 2),
        });

        var train = new Train(5);

        Assert.Throws<InvalidGamePlayException>(() => player.PlayDomino(0, train));
    }

    [Fact]
    public void PlayDomino_ThrowsException_WhenPlayerHasNoMoreDominoes()
    {
        var player = new Player(new List<Domino>());

        var train = new Train(5);

        Assert.Throws<InvalidGamePlayException>(() => player.PlayDomino(0, train));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1)]
    public void PlayDomino_ThrowsException_WhenIndexIsOutOfRange(int index)
    {
        var player = new Player(new List<Domino>
        {
            new (1, 2),
        });

        var train = new Train(5);

        Assert.Throws<InvalidGamePlayException>(() => player.PlayDomino(index, train));
    }
}