using System.Reflection;
using DominoTrains.Application.Factories;
using DominoTrains.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DominoTrains.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.TryAddScoped<IDominoesFactory, UniqueZeroToSixDominoesFactory>();
        return services;
    }
}