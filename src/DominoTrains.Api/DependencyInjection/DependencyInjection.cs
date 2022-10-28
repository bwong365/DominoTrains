using System.Text.Json.Serialization;
using System.Reflection;

namespace DominoTrains.Api.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            // Without this, CreatedAtAction fails when referencing Async-Suffixed controller methods
            opt.SuppressAsyncSuffixInActionNames = false;
        }).AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            var xmlCommentsFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFileName);
            opt.IncludeXmlComments(xmlCommentsFullPath);
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}