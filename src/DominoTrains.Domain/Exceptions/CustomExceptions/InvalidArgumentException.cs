using DominoTrains.Domain.Exceptions.BaseExceptions;

namespace DominoTrains.Domain.Exceptions.CustomExceptions;

public class InvalidArgumentException : ViolationException
{
    public InvalidArgumentException(string message, string? paramName = null) : base($"Invalid argument: {(string.IsNullOrWhiteSpace(paramName) ? "" : $"({paramName})")} {message}")
    {
    }
}
