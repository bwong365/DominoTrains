namespace DominoTrains.Domain.Exceptions.BaseExceptions;

public abstract class UnexpectedException : Exception
{
    public UnexpectedException(string message) : base(message)
    {
    }
}
