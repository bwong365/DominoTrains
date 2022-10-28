namespace DominoTrains.Api.ViewModels;

public record DominoViewModel
{
    /// <summary>
    /// The number of dots on one side of the domino
    /// </summary>
    public int A { get; init; }

    /// <summary>
    /// The number of dots on the other side of the domino
    /// </summary>
    public int B { get; init; }
}