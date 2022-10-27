namespace DominoTrains.Domain.Exceptions.BaseExceptions;

public abstract class ViolationException : Exception
{
    public ViolationException(string message) : base(message)
    {
    }
}