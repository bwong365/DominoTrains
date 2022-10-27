using DominoTrains.Domain.Exceptions.BaseExceptions;

namespace DominoTrains.Domain.Exceptions.CustomExceptions;

public class GameSetupException : UnexpectedException
{
    public GameSetupException(string message) : base("There was an error setting up the game: " + message)
    {
    }
}
