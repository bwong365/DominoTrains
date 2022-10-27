using DominoTrains.Domain.Exceptions.CustomExceptions;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Aggregates;

public class Game
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Player Player { get; private set; } = null!;
    public TrainStation TrainStation { get; private set; } = null!;

    public static Game NewGame(List<Domino> dominoList, Domino startingDomino, int handSize)
    {
        if (dominoList.Count < 1)
        {
            throw new GameSetupException("There must be at least one domino to play with");
        }

        if (handSize > dominoList.Count || handSize <= 0)
        {
            throw new GameSetupException("Hand size must be greater than 0 and less than the number of dominoes");
        }

        var trainStation = new TrainStation(startingDomino);

        var random = new Random();
        var hand = dominoList
            .OrderBy(d => random.Next())
            .Take(handSize)
            .ToList();
        var player = new Player(hand);

        return new Game(trainStation, player);
    }

    private Game(TrainStation trainStation, Player player)
    {
        Player = player;
        TrainStation = trainStation;
    }

    private Game() { }

    public bool IsGameOver()
    {
        return Player.Dominoes.Count == 0 || !HasValidMoves();
    }

    private bool HasValidMoves() => Player.Dominoes
        .Any(d => TrainStation.North.CanPlay(d) ||
            TrainStation.East.CanPlay(d) ||
            TrainStation.West.CanPlay(d) ||
            TrainStation.South.CanPlay(d));
}