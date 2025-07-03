using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Cinema.Catalog.Domain.Shared;

[ExcludeFromCodeCoverage]
public static class OptionsSetup
{
    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainOptions<TmdbApiOptions>(configuration, "TmdbApi");
    }

    private static void AddDomainOptions<T>(this IServiceCollection services, IConfiguration configuration, string section) where T : class
    {
        services.AddOptions<T>()
            .Bind(configuration.GetSection(section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}


[ExcludeFromCodeCoverage]
public class TmdbApiOptions
{
    public string Name => "TmdbApi";
    public string Language => "pt-BR";
    [Required]
    public string BaseUrl { get; set; } = string.Empty;
    [Required]
    public string ApiKey { get; set; } = string.Empty;
    [Required]
    public string AuthToken { get; set; } = string.Empty;
}