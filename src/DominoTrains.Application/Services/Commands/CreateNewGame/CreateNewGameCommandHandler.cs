using DominoTrains.Application.Interfaces;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.ValueObjects;
using MediatR;

namespace DominoTrains.Application.Services.Commands.CreateNewGame;

public class CreateNewGameCommandHandler : IRequestHandler<CreateNewGameCommand, Game>
{
    private readonly IDominoesFactory _dominoesFactory;
    private readonly IGameRepository _gameRepository;
    private const int HandSize = 10;

    public CreateNewGameCommandHandler(IDominoesFactory dominoesFactory, IGameRepository gameRepository)
    {
        _dominoesFactory = dominoesFactory;
        _gameRepository = gameRepository;
    }

    public async Task<Game> Handle(CreateNewGameCommand request, CancellationToken ct)
    {
        var dominoes = _dominoesFactory.CreateDominoes();
        var startingDomino = PopLastDomino(dominoes);

        var game = Game.NewGame(dominoes, startingDomino, HandSize);

        await _gameRepository.AddGameAsync(game, ct);

        return game;
    }

    private Domino PopLastDomino(List<Domino> dominoes)
    {
        var lastDominoIndex = dominoes.Count - 1;
        var lastDomino = dominoes[lastDominoIndex];
        dominoes.RemoveAt(lastDominoIndex);
        return lastDomino;
    }
}