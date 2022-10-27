using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Enums;
using MediatR;

namespace DominoTrains.Application.Services.Commands.PlayDomino;

public record PlayDominoCommand : IRequest<Game>
{
    public Guid GameId { get; init; }
    public int DominoIndex { get; init; }
    public Direction Direction { get; init; }
}