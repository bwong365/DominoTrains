using DominoTrains.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DominoTrains.Api.IntegrationTest;

public class DominoTrainsApi : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Disable Serilog for tests, otherwise it will prevent multiple test classes from running
        builder.UseSerilog((_, _) => { });

        // Not ideal to use the in memory db, but with zero infrastructure for this MVP, this is good enough!
        var root = new InMemoryDatabaseRoot();
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Testing", root);
            });
        });
        return base.CreateHost(builder);
    }
}