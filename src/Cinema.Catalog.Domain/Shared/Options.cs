using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Shared;

/// <summary>
/// Classe utilitária responsável pela configuração centralizada de opções (options) do domínio.
/// </summary>
/// <remarks>
/// Fornece métodos de extensão para registrar e validar opções de configuração no container de injeção de dependência.
/// Utiliza data annotations para validação automática das opções durante o startup da aplicação.
/// </remarks>
[ExcludeFromCodeCoverage]
public static class OptionsSetup
{
    /// <summary>
    /// Registra e configura as opções do domínio, vinculando-as ao container de serviços.
    /// </summary>
    /// <param name="services">Coleção de serviços da aplicação.</param>
    /// <param name="configuration">Configuração da aplicação (ex: appsettings.json).</param>
    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainOptions<TmdbApiOptions>(configuration, "TmdbApi");
    }

    /// <summary>
    /// Método auxiliar para registrar e validar uma seção de opções do domínio.
    /// </summary>
    /// <typeparam name="T">Tipo da classe de opções.</typeparam>
    /// <param name="services">Coleção de serviços da aplicação.</param>
    /// <param name="configuration">Configuração da aplicação.</param>
    /// <param name="section">Nome da seção de configuração.</param>
    private static void AddDomainOptions<T>(this IServiceCollection services, IConfiguration configuration, string section) where T : class
    {
        services.AddOptions<T>()
            .Bind(configuration.GetSection(section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}

/// <summary>
/// Modelo de opções para configuração da integração com a API TMDb.
/// </summary>
/// <remarks>
/// Utilizado para mapear e validar as configurações necessárias para comunicação com a API do The Movie Database (TMDb).
/// Os valores são lidos da seção <c>TmdbApi</c> do arquivo de configuração da aplicação.
/// </remarks>
[ExcludeFromCodeCoverage]
public class TmdbApiOptions
{
    /// <summary>
    /// Nome da seção de configuração.
    /// </summary>
    public string Name => "TmdbApi";

    /// <summary>
    /// Idioma padrão utilizado nas requisições para a API TMDb.
    /// </summary>
    public string Language => "pt-BR";

    /// <summary>
    /// URL base da API TMDb.
    /// </summary>
    [Required]
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Chave de API utilizada para autenticação nas requisições TMDb.
    /// </summary>
    [Required]
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Token de autenticação utilizado para requisições seguras na API TMDb.
    /// </summary>
    [Required]
    public string AuthToken { get; set; } = string.Empty;
}
