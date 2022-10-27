using DominoTrains.Application.Interfaces;
using DominoTrains.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DominoTrains.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // EF Core's in-memory database is great for a demo, but insufficient for testing whether the data model
        // will actually work with a real db; therefore testing was done using PostgreSQL. I've left the code 
        // and nuget references here in place for verification/demonstration purposes.
        // services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DominoTrainsDb")));

        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DominoTrains"));
        services.TryAddScoped<IGameRepository, GameRepository>();
        return services;
    }
}