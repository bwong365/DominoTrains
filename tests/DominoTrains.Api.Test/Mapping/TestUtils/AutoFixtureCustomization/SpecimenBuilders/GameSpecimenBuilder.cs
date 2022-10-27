using AutoFixture.Kernel;
using DominoTrains.Application.Factories;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.Test.Mapping.TestUtils.AutoFixtureCustomization.SpecimenBuilders;

public class GameSpecimenBuilder : ISpecimenBuilder
{
    private readonly Random _random;

    public GameSpecimenBuilder()
    {
        _random = new Random();
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(Game))
        {
            return new NoSpecimen();
        }

        var value = _random.Next(Domino.MinValue, Domino.MaxValue + 1);
        var trainStation = new TrainStation(new(value, value));

        var dominoes = new UniqueZeroToSixDominoesFactory().CreateDominoes();
        return Game.NewGame(dominoes, new Domino(0, 0), 10);
    }
}