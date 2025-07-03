using Cinema.Catalog.Domain.Shared;
using Cinema.Catalog.Infrastructure.HttpClients.GatewayAdapters;
using Cinema.Catalog.Infrastructure.HttpClients.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace Cinema.Catalog.Infrastructure.HttpClients;

public static class Setup
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTmdbApiApiHttpClient(configuration);
        services.AddHandlersHttp();
        services.AddGateways();
    }

    private static void AddTmdbApiApiHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var tmdbApiOptions = configuration.GetSection("TmdbApi").Get<TmdbApiOptions>();

        services.AddHttpClient(tmdbApiOptions!.Name, httpClient =>
        {
            httpClient.BaseAddress = new Uri(tmdbApiOptions.BaseUrl);
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddHttpMessageHandler<AuthenticateCustomHandler<AuthBasicImdb>>();
    }

    private static void AddGateways(this IServiceCollection services)
    {
        services.AddTransient<AuthBasicImdb>();
    }

    private static void AddHandlersHttp(this IServiceCollection services)
    {
        services.AddTransient<AuthenticateCustomHandler<AuthBasicImdb>>();
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
    }
}
