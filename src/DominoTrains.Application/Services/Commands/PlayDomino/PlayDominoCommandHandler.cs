using DominoTrains.Application.Interfaces;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Enums;
using MediatR;

namespace DominoTrains.Application.Services.Commands.PlayDomino;

public class PlayDominoCommandHandler : IRequestHandler<PlayDominoCommand, Game>
{
    private readonly IGameRepository _gameRepository;

    public PlayDominoCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> Handle(PlayDominoCommand request, CancellationToken ct)
    {
        var game = await _gameRepository.GetGameAsync(request.GameId, ct);
        if (game is null)
        {
            throw new ApplicationException($"Game with id {request.GameId} not found");
        }

        var train = request.Direction switch
        {
            Direction.North => game.TrainStation.North,
            Direction.East => game.TrainStation.East,
            Direction.South => game.TrainStation.South,
            Direction.West => game.TrainStation.West,
            _ => throw new ArgumentException($"Invalid direction {request.Direction}")
        };

        if (game.IsGameOver())
        {
            throw new ApplicationException("Game is over");
        }

        game.Player.PlayDomino(request.DominoIndex, train);
        await _gameRepository.SaveChangesAsync(ct);
        return game;
    }

}