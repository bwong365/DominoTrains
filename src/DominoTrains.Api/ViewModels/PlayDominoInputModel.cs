using DominoTrains.Api.ViewModels.Enums;

namespace DominoTrains.Api.ViewModels;

public record PlayDominoInputModel
{
    public int DominoIndex { get; init; }
    public Direction Direction { get; init; }
}