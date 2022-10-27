using AutoMapper;
using DominoTrains.Api.Mapping;

namespace DominoTrains.Api.Test.Mapping;

public class MappingProfileTests
{
    [Fact]
    public void ConfigurationIsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        config.AssertConfigurationIsValid();
    }
}