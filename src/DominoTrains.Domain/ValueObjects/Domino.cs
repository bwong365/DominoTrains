using DominoTrains.Domain.Exceptions.CustomExceptions;

namespace DominoTrains.Domain.ValueObjects;

public record Domino
{
    public const int MinValue = 0;
    public const int MaxValue = 6;

    public int A { get; private set; }
    public int B { get; private set; }

    public Domino(int a, int b)
    {
        if (a < MinValue || a > MaxValue)
        {
            throw new GameSetupException($"Domino value must be between {MinValue} and {MaxValue}");
        }

        if (b < MinValue || b > MaxValue)
        {
            throw new GameSetupException($"Domino value must be between {MinValue} and {MaxValue}");
        }

        A = a;
        B = b;
    }

    public int TotalDots => A + B;
}