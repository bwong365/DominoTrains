using DominoTrains.Api.DependencyInjection;
using DominoTrains.Application.DependencyInjection;
using DominoTrains.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApi()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
