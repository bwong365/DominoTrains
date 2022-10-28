using DominoTrains.Api.ViewModels.Enums;

namespace DominoTrains.Api.ViewModels;

public record GameViewModel
{
    /// <summary>
    /// The unique identifier of the game
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The current state of the game
    /// </summary>
    public GameStatus Status { get; init; }

    /// <summary>
    /// The number of dots who's turn it is to play
    /// </summary>
    public int DotsInHand { get; init; }

    /// <summary>
    /// The parent object of the domino trains
    /// </summary>
    public TrainStationViewModel TrainStation { get; init; } = null!;

    /// <summary>
    /// The dominoes available to the player for gameplay
    /// </summary>
    public List<DominoViewModel> Hand { get; init; } = new();
}