using DominoTrains.Domain.Exceptions.BaseExceptions;

namespace DominoTrains.Domain.Exceptions.CustomExceptions;

public class InvalidGamePlayException : ViolationException
{
    public InvalidGamePlayException(string message) : base("Invalid move: " + message)
    {
    }
}
