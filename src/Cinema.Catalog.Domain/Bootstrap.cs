using Cinema.Catalog.Domain.Services;
using Cinema.Catalog.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain;

[ExcludeFromCodeCoverage]
public static class Bootstrap
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
    }
}
