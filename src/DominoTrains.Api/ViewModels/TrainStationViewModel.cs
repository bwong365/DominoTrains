namespace DominoTrains.Api.ViewModels;

public record TrainStationViewModel
{
    /// <summary>
    /// This train's picking up presents from the North Pole!
    /// </summary>
    public TrainViewModel North { get; init; } = null!;

    /// <summary>
    /// This train's heading to the Atlantic
    /// </summary>
    public TrainViewModel East { get; init; } = null!;

    /// <summary>
    /// This train decided to head south for the winter
    /// </summary>
    public TrainViewModel South { get; init; } = null!;

    /// <summary>
    /// This train's heading for the Rocky mountains!
    /// </summary>
    public TrainViewModel West { get; init; } = null!;
}