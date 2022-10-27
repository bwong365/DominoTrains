using DominoTrains.Domain.Aggregates;
using MediatR;

namespace DominoTrains.Application.Services.Queries.GetGame;

public class GetGameQuery : IRequest<Game?>
{
    public Guid GameId { get; set; }
}