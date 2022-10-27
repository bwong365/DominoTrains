using DominoTrains.Application.Interfaces;
using DominoTrains.Application.Services.Commands.CreateNewGame;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.ValueObjects;
using Moq;

namespace DominoTrains.Application.Test.Services.CreateNewGame;

public class CreateNewGameCommandHandlerTests
{
    private readonly Mock<IDominoesFactory> _mockDominoesFactory;
    private readonly Mock<IGameRepository> _mockGameRepository;
    private readonly CreateNewGameCommandHandler _sut;

    public CreateNewGameCommandHandlerTests()
    {
        _mockDominoesFactory = new Mock<IDominoesFactory>();
        _mockGameRepository = new Mock<IGameRepository>();
        _sut = new CreateNewGameCommandHandler(_mockDominoesFactory.Object, _mockGameRepository.Object);
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

        _mockDominoesFactory.Setup(x => x.CreateDominoes()).Returns(dominoes);
        _mockGameRepository
            .Setup(x => x.AddGameAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Callback<Game, CancellationToken?>((game, _) => persistedGame = game);

        var result = await _sut.Handle(new CreateNewGameCommand(), CancellationToken.None);

        Assert.Equal(result, persistedGame);
    }
}