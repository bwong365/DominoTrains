using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Test.Models;

public class GameTests
{
    private readonly List<Domino> _dominoList;
    private readonly Domino _startingDomino;
    private readonly int _handSize;

    public GameTests()
    {
        _dominoList = new List<Domino>
        {
            new (1, 1),
            new (2, 2),
            new (3, 3),
        };
        _startingDomino = new Domino(1, 1);
        _handSize = 1;
    }

    [Fact]
    public void CanBeCreated_With_ValidArguments()
    {
        var game = Game.NewGame(_dominoList, _startingDomino, _handSize);

        Assert.NotNull(game);
    }

    [Fact]
    public void CannotBeCreated_With_TooFewDominoes()
    {
        var dominoList = new List<Domino>();

        var creationAttempt = () => Game.NewGame(dominoList, _startingDomino, _handSize);

        Assert.Throws<ArgumentOutOfRangeException>(() => creationAttempt());
    }

    [Theory]
    [InlineData(70)]
    [InlineData(0)]
    public void CannotBeCreated_With_AnInvalidHandSize(int handSize)
    {
        var creationAttempt = () => Game.NewGame(_dominoList, _startingDomino, handSize);

        Assert.Throws<ArgumentOutOfRangeException>(() => creationAttempt());
    }

    [Fact]
    public void IsGameOver_ReturnsTrue_WhenPlayerHasNoValidMoves()
    {
        var dominoList = new List<Domino>
        {
            new (1, 1),
            new (2, 2),
        };
        var startingDomino = new Domino(0, 0);

        var game = Game.NewGame(dominoList, startingDomino, 2);

        Assert.True(game.IsGameOver());
    }

    [Fact]
    public void IsGameOver_ReturnsFalse_WhenPlayerHasValidMoves()
    {
        var dominoList = new List<Domino>
        {
            new (1, 0),
            new (2, 0),
        };
        var startingDomino = new Domino(0, 0);

        var game = Game.NewGame(dominoList, startingDomino, 2);

        Assert.False(game.IsGameOver());
    }
}