using DominoTrains.Api.DependencyInjection;
using DominoTrains.Api.Middleware;
using DominoTrains.Application.DependencyInjection;
using DominoTrains.Infrastructure.DependencyInjection;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("startup.txt")
    .CreateBootstrapLogger();

Log.Information("Starting up");

// Ensure that startup issues are logged
try
{
    Startup(args);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

static void Startup(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((ctx, config) => config
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services
        .AddApi()
        .AddInfrastructure(builder.Configuration)
        .AddApplication();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseSerilogRequestLogging();
    app.UseGlobalErrorHandlerMiddleware();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

// Exposing this class allows the application to be started from the integrations test project
// https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0#test-with-webapplicationfactory-or-testserver
public partial class Program { }