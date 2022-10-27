using System.Net;
using DominoTrains.Api.IntegrationTest.Utils;
using DominoTrains.Api.ViewModels;
using DominoTrains.Api.ViewModels.Enums;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.IntegrationTest.Controllers;

[Trait("Category", "Integration")]
public class DominoTrainsControllerTests : IDisposable
{
    private DominoTrainsApi _application;
    private readonly HttpClient _client;

    public DominoTrainsControllerTests()
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
    public async Task CreateGameAsync_CreatesGame()
    {
        var response = await _client.PostAsync("/dominoTrains", null);
        var game = await response.DeserializeAsync<GameViewModel>();

        Assert.NotEmpty(game?.Hand);
    }

    [Fact]
    public async Task GetGameAsync_ReturnsExistingGame()
    {
        var game = await CreateGameAsync();

        var getResponse = await _client.GetAsync($"/dominoTrains/{game!.Id}");

        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetGameAsync_ReturnsNotFound_WhenNonExistent()
    {
        var getResponse = await _client.GetAsync($"/dominoTrains/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task PlayDominoAsync_PlaysDomino()
    {
        var (game, dominoIndex) = await CreateWinnableGameAsync();
        var body = new PlayDominoInputModel
        {
            DominoIndex = dominoIndex,
            Direction = Direction.North
        }.ToStringContent();

        var playResponse = await _client.PostAsync($"/dominoTrains/{game!.Id}/playDomino", body);

        playResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task PlayDominoAsync_FailsWith_NonExistentDomino()
    {
        var game = await CreateGameAsync();
        var body = new PlayDominoInputModel
        {
            DominoIndex = -1,
        }.ToStringContent();

        var playResponse = await _client.PostAsync($"/dominoTrains/{game!.Id}/playDomino", body);
        Assert.Equal(HttpStatusCode.BadRequest, playResponse.StatusCode);
    }

    [Fact]
    public async Task PlayDominoAsync_FailsWith_DominoWithoutMatchingEdge()
    {
        var game = await CreateGameAsync();
        var dominoIndex = game!.Hand.FindIndex(d => d.A != Domino.MaxValue && d.B != Domino.MaxValue);
        var body = new PlayDominoInputModel
        {
            DominoIndex = dominoIndex,
        }.ToStringContent();

        var playResponse = await _client.PostAsync($"/dominoTrains/{game!.Id}/playDomino", body);
        Assert.Equal(HttpStatusCode.BadRequest, playResponse.StatusCode);
    }

    private async Task<GameViewModel?> CreateGameAsync()
    {
        var creationResponse = await _client.PostAsync("/dominoTrains", null);
        return await creationResponse.DeserializeAsync<GameViewModel>();
    }

    private async Task<(GameViewModel, int)> CreateWinnableGameAsync()
    {
        while (true)
        {
            var game = await CreateGameAsync();
            var dominoIndex = game!.Hand.FindIndex(d => d.A == Domino.MaxValue || d.B == Domino.MaxValue);

            if (dominoIndex >= 0)
            {
                return (game, dominoIndex);
            }
        }
    }
}