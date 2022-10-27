namespace DominoTrains.Domain.Exceptions.BaseExceptions;

public abstract class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}