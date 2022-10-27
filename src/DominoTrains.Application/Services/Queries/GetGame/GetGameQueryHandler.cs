using DominoTrains.Application.Interfaces;
using DominoTrains.Domain.Aggregates;
using MediatR;

namespace DominoTrains.Application.Services.Queries.GetGame;

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, Game?>
{
    private readonly IGameRepository _gameRepository;

    public GetGameQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game?> Handle(GetGameQuery request, CancellationToken ct)
    {
        var game = await _gameRepository.GetGameAsync(request.GameId, ct);
        if (game is null)
        {
            throw new KeyNotFoundException($"Game with id {request.GameId} not found");
        }

        return game;
    }
}
