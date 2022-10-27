using DominoTrains.Domain.Aggregates;
using MediatR;

namespace DominoTrains.Application.Services.Commands.CreateNewGame;

public record CreateNewGameCommand : IRequest<Game>
{
}