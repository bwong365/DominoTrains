using DominoTrains.Domain.Exceptions.BaseExceptions;

namespace DominoTrains.Domain.Exceptions.CustomExceptions;

public class GameNotFoundException : NotFoundException
{
    public GameNotFoundException() : base("Game not found")
    {
    }
}