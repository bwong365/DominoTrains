using DominoTrains.Api.ViewModels.Enums;

namespace DominoTrains.Api.ViewModels;

public record PlayDominoInputModel
{
    /// <summary>
    /// The array index of the domino to play from the player's hand
    /// </summary>
    public int DominoIndex { get; init; }

    /// <summary>
    /// The train to play the domino on, referenced by the property on the TrainStation
    /// </summary>
    public Direction Direction { get; init; }
}