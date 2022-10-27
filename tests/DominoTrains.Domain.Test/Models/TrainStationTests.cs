using DominoTrains.Domain.Enums;
using DominoTrains.Domain.Exceptions.CustomExceptions;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Test.Models;

public class TrainStationTests
{
    [Theory]
    [InlineData(6, 6)]
    [InlineData(1, 1)]
    public void CanBeCreated_With_ValidArguments(int a, int b)
    {
        var domino = new Domino(a, b);
        var trainStation = new TrainStation(domino);

        Assert.Equal(domino.A, trainStation.North.EdgeValue);
        Assert.Equal(domino.A, trainStation.East.EdgeValue);
        Assert.Equal(domino.A, trainStation.West.EdgeValue);
        Assert.Equal(domino.A, trainStation.South.EdgeValue);
    }

    [Fact]
    public void CannotBeCreated_With_InvalidArguments()
    {
        var domino = new Domino(6, 4);
        var creationAttempt = () => new TrainStation(domino);

        Assert.Throws<GameSetupException>(() => creationAttempt());
    }

    [Theory]
    [InlineData(Direction.North)]
    [InlineData(Direction.East)]
    [InlineData(Direction.South)]
    [InlineData(Direction.West)]
    public void GetTrain_GetsCorrectTrain(Direction direction)
    {
        var domino = new Domino(5, 5);
        var trainStation = new TrainStation(domino);

        var trainResult = trainStation.GetTrain(direction);

        var directionString = direction.ToString();
        var expectedTrain = trainStation.GetType().GetProperty(directionString)?.GetValue(trainStation);
        Assert.Same(expectedTrain, trainResult);
    }
}