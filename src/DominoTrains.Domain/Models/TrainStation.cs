using DominoTrains.Domain.Enums;
using DominoTrains.Domain.Exceptions.CustomExceptions;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Models;

public class TrainStation
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Train North { get; private set; } = null!;
    public Train East { get; private set; } = null!;
    public Train West { get; private set; } = null!;
    public Train South { get; private set; } = null!;

    public TrainStation(Domino domino)
    {
        if (domino.A != domino.B)
        {
            throw new GameSetupException("Domino must be symmmetrical");
        }

        North = new Train(domino.A);
        East = new Train(domino.A);
        West = new Train(domino.A);
        South = new Train(domino.A);
    }

    private TrainStation()
    {
    }

    public Train GetTrain(Direction direction)
    {
        return direction switch
        {
            Direction.North => North,
            Direction.East => East,
            Direction.West => West,
            Direction.South => South,
            _ => throw new InvalidArgumentException("Invalid direction"),
        };
    }
}