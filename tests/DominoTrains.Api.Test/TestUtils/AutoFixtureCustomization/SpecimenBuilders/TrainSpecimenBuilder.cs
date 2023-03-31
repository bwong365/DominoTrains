using AutoFixture.Kernel;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.Test.TestUtils.AutoFixtureCustomization.SpecimenBuilders;

public class TrainSpecimenBuilder : ISpecimenBuilder
{
    private readonly Random _random;

    public TrainSpecimenBuilder()
    {
        _random = new Random();
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(Train))
        {
            return new NoSpecimen();
        }

        return new Train(_random.Next(Domino.MinValue, Domino.MaxValue + 1));
    }
}