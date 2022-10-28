using DominoTrains.Api.IntegrationTest.Utils;
using DominoTrains.Api.ViewModels;
using DominoTrains.Api.ViewModels.Enums;

namespace DominoTrains.Api.IntegrationTest.Gameplay;

[Trait("Category", "Integration")]
public class GameplayTest : IDisposable
{
    private DominoTrainsApi _application;
    private readonly HttpClient _client;

    public GameplayTest()
    {
        _application = new DominoTrainsApi();
        _client = _application.CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
        _application.Dispose();
    }

    [Fact]
    public async Task GameContinues_Until_NoValidMoves()
    {
        GameViewModel? game;
        var creationResponse = await _client.PostAsync("/dominoTrains", null);
        game = await creationResponse.DeserializeAsync<GameViewModel>();
        if (game is null)
        {
            throw new Exception("Game was not created");
        }

        var gameId = game.Id;

        while (game!.Status == GameStatus.Active)
        {
            var move = GetValidMove(game);
            if (move is null)
            {
                throw new Exception("Status should not have been active");
            }
            var (dominoIndex, direction) = move.Value;

            var body = new PlayDominoInputModel
            {
                DominoIndex = dominoIndex,
                Direction = direction
            }.ToStringContent();

            var playResponse = await _client.PostAsync($"/dominoTrains/{gameId}/playDomino", body);
            game = await playResponse.DeserializeAsync<GameViewModel>();
            if (game is null)
            {
                throw new Exception("Gameplay broke down");
            }
        }

        Assert.Equal(GameStatus.Complete, game.Status);
        Assert.Null(GetValidMove(game));
        Assert.Equal(game.Hand.Sum(d => d.A + d.B), game.DotsInHand);
    }

    private (int dominoIndex, Direction direction)? GetValidMove(GameViewModel game)
    {
        var dominoesWithIndex = game.Hand.Select((domino, index) => (domino, index)).ToList();
        foreach (var (domino, index) in dominoesWithIndex)
        {
            foreach (var direction in Enum.GetValues<Direction>())
            {
                var train = game.TrainStation.GetType().GetProperty(direction.ToString())?.GetValue(game.TrainStation) as TrainViewModel;
                if (train?.EdgeValue == domino.A || train?.EdgeValue == domino.B)
                {
                    return (index, direction);
                }
            }
        }

        return null;
    }
}