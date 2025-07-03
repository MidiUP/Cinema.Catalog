using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Infrastructure.ApiFacades;
using Cinema.Catalog.Infrastructure.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Catalog.Infrastructure;

public static class Bootstrap
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClients(configuration);
        services.AddApiFacades();
    }

    private static void AddApiFacades(this IServiceCollection services)
    {
        services.AddScoped<ITmdbApiFacade, TmdbApiFacade>();
    }
}
