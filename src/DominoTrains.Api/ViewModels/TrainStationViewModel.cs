namespace DominoTrains.Api.ViewModels;

public record TrainStationViewModel
{
    public TrainViewModel North { get; init; } = null!;
    public TrainViewModel East { get; init; } = null!;
    public TrainViewModel South { get; init; } = null!;
    public TrainViewModel West { get; init; } = null!;
}