using System.Text.Json;
using System.Text.Json.Serialization;

namespace DominoTrains.Api.IntegrationTest;

public class DefaultJsonSerializerOptionsFactory
{
    public JsonSerializerOptions Create()
    {
        return new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}