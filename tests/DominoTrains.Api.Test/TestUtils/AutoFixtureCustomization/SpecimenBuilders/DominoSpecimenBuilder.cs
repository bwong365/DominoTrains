using AutoFixture.Kernel;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.Test.TestUtils.AutoFixtureCustomization.SpecimenBuilders;

public class DominoSpecimenBuilder : ISpecimenBuilder
{
    private readonly Random _random;

    public DominoSpecimenBuilder()
    {
        _random = new Random();
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(Domino))
        {
            return new NoSpecimen();
        }

        return new Domino(_random.Next(Domino.MinValue, Domino.MaxValue + 1), _random.Next(Domino.MinValue, Domino.MaxValue + 1));
    }
}