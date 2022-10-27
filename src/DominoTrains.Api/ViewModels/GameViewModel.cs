namespace DominoTrains.Api.ViewModels;

public record GameViewModel
{
    public Guid Id { get; init; }
    public GameStatus Status { get; init; }
    public int DotsInHand { get; init; }
    public TrainStationViewModel TrainStation { get; init; } = null!;
    public List<DominoViewModel> Hand { get; init; } = new();

}