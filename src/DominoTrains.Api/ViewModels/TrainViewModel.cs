namespace DominoTrains.Api.ViewModels;

public record TrainViewModel
{
    /// <summary>
    /// The number of dots on the playable edge of the train
    /// One of the domino's sides must match this value
    /// </summary>
    public int EdgeValue { get; init; }

    /// <summary>
    /// The dominoes already placed in the train
    /// </summary>
    public List<DominoViewModel> Dominoes { get; init; } = new();
}