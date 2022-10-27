using AutoMapper;
using DominoTrains.Api.Mapping;
using DominoTrains.Api.Test.Mapping.TestUtils.AutoFixtureCustomization;
using DominoTrains.Api.ViewModels;
using DominoTrains.Api.ViewModels.Enums;
using DominoTrains.Application.Services.Commands.PlayDomino;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.Test.Mapping;

public class MappingProfileTests
{
    private readonly MapperConfiguration _config;
    private readonly Mapper _mapper;

    public MappingProfileTests()
    {
        _config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = new Mapper(_config);
    }

    [Fact]
    public void ConfigurationIsValid()
    {
        _config.AssertConfigurationIsValid();
    }

    // The above configuration validation should be enough for most cases, 
    // but it's good to be a little bit paranoid when using AutoMapper.

    [Theory, CustomAutoData]
    public void MapsDominoCorrectly(Domino domino)
    {
        var result = _mapper.Map<DominoViewModel>(domino);

        Assert.Equal(domino.A, result.A);
        Assert.Equal(domino.B, result.B);
    }

    [Theory, CustomAutoData]
    public void MapsTrainCorrectly(Train train)
    {
        var result = _mapper.Map<TrainViewModel>(train);

        Assert.Equal(train.Dominoes.Count, result.Dominoes.Count);
        Assert.Equal(train.EdgeValue, result.EdgeValue);
    }

    [Theory, CustomAutoData]
    public void MapsTrainStationCorrectly(TrainStation trainStation)
    {
        var result = _mapper.Map<TrainStationViewModel>(trainStation);

        Assert.Equal(trainStation.North.EdgeValue, result.North.EdgeValue);
        Assert.Equal(trainStation.East.EdgeValue, result.East.EdgeValue);
        Assert.Equal(trainStation.West.EdgeValue, result.West.EdgeValue);
        Assert.Equal(trainStation.South.EdgeValue, result.South.EdgeValue);
    }

    [Theory, CustomAutoData]
    public void MapsGameCorrectly(Game game)
    {
        var result = _mapper.Map<GameViewModel>(game);

        Assert.Equal(game.Player.Dominoes.Count, result.Hand.Count);
        Assert.Equal(game.Player.GetDotsInHand(), result.DotsInHand);
        Assert.Equal(game.IsGameOver(), result.Status == GameStatus.Complete);
    }

    [Theory, CustomAutoData]
    public void MapsPlayDominoCommandCorrectly(Guid gameId, PlayDominoInputModel input)
    {
        var result = _mapper.Map<PlayDominoCommand>((gameId, input));

        Assert.Equal(gameId, result.GameId);
        Assert.Equal(input.Direction.ToString(), result.Direction.ToString());
        Assert.Equal(input.DominoIndex, result.DominoIndex);
    }
}