using AutoFixture.Kernel;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.Test.TestUtils.AutoFixtureCustomization.SpecimenBuilders;

public class TrainStationSpecimenBuilder : ISpecimenBuilder
{
    private readonly Random _random;

    public TrainStationSpecimenBuilder()
    {
        _random = new Random();
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(TrainStation))
        {
            return new NoSpecimen();
        }

        var value = _random.Next(Domino.MinValue, Domino.MaxValue + 1);
        return new TrainStation(new(value, value));
    }
}