using DominoTrains.Application.Interfaces;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Enums;
using DominoTrains.Domain.Exceptions.CustomExceptions;
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
            throw new GameNotFoundException();
        }

        var train = request.Direction switch
        {
            Direction.North => game.TrainStation.North,
            Direction.East => game.TrainStation.East,
            Direction.South => game.TrainStation.South,
            Direction.West => game.TrainStation.West,
            _ => throw new InvalidArgumentException($"Invalid direction {request.Direction}")
        };

        if (game.IsGameOver())
        {
            throw new InvalidGamePlayException("Game is over");
        }

        game.Player.PlayDomino(request.DominoIndex, train);
        await _gameRepository.SaveChangesAsync(ct);
        return game;
    }

}