namespace DominoTrains.Api.ViewModels;

public record TrainViewModel
{
    public int EdgeValue { get; init; }
    public List<DominoViewModel> Dominoes { get; init; } = new();
}