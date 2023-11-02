using DominoTrains.Application.Interfaces;
using DominoTrains.Application.Services.Commands.CreateNewGame;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.ValueObjects;
using NSubstitute;

namespace DominoTrains.Application.Test.Services.CreateNewGame;

public class CreateNewGameCommandHandlerTests
{
    private readonly IDominoesFactory _mockDominoesFactory;
    private readonly IGameRepository _mockGameRepository;
    private readonly CreateNewGameCommandHandler _sut;

    public CreateNewGameCommandHandlerTests()
    {
        _mockDominoesFactory = Substitute.For<IDominoesFactory>();
        _mockGameRepository = Substitute.For<IGameRepository>();
        _sut = new CreateNewGameCommandHandler(_mockDominoesFactory, _mockGameRepository);
    }

    [Fact]
    public async Task Handle_SavesCreatedGame()
    {
        var dominoes = new List<Domino>
        {
            new (0, 0), new (0, 1), new (0, 2), new (0, 3), new (0, 4), new (0, 5), new (0, 6),
            new (1, 1), new (1, 2), new (1, 3), new (1, 4), new (1, 5), new (1, 6), new (6, 6)
        };
        Game? persistedGame = null;

        _mockDominoesFactory.CreateDominoes().Returns(dominoes);
        _mockGameRepository.AddGameAsync(Arg.Any<Game>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask).AndDoes(x => persistedGame = x.Arg<Game>());
        var result = await _sut.Handle(new CreateNewGameCommand(), CancellationToken.None);

        Assert.Equal(result, persistedGame);
    }
}