using AutoFixture;
using DominoTrains.Api.Test.Mapping.TestUtils.AutoFixtureCustomization.SpecimenBuilders;

namespace DominoTrains.Api.Test.Mapping.TestUtils.AutoFixtureCustomization;

public class DominoTrainCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new DominoSpecimenBuilder());
        fixture.Customizations.Add(new TrainSpecimenBuilder());
        fixture.Customizations.Add(new TrainStationSpecimenBuilder());
        fixture.Customizations.Add(new GameSpecimenBuilder());
    }
}