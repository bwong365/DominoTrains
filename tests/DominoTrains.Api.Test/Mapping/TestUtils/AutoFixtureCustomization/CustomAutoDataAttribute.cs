using AutoFixture;
using AutoFixture.Xunit2;

namespace DominoTrains.Api.Test.Mapping.TestUtils.AutoFixtureCustomization;

public sealed class CustomAutoDataAttribute : AutoDataAttribute
{
    public CustomAutoDataAttribute() : base(CreateFixture)
    {
    }

    private static Fixture CreateFixture()
    {
        var fixture = new Fixture();

        fixture.Customize(new DominoTrainCustomization());

        return fixture;
    }
}
