using System.Text;
using System.Text.Json;

namespace DominoTrains.Api.IntegrationTest.Utils;

/**
* The purpose of these extensions is to ensure the DefaultJsonSerializerOptions are used
*/
public static class SerializerExtensions
{
    public async static Task<T?> DeserializeAsync<T>(this HttpResponseMessage responseMessage)
    {
        var stream = await responseMessage.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(stream, new DefaultJsonSerializerOptionsFactory().Create());
    }

    public static HttpContent ToStringContent<T>(this T obj)
    {
        if (obj is null)
        {
            return new StringContent(string.Empty);
        }

        var json = JsonSerializer.Serialize<T>(obj, new DefaultJsonSerializerOptionsFactory().Create());
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}