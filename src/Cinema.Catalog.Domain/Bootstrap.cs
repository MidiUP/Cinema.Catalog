using Cinema.Catalog.Domain.Services;
using Cinema.Catalog.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain;

/// <summary>
/// Classe utilitária responsável pelo registro dos serviços de domínio no container de injeção de dependência.
/// </summary>
/// <remarks>
/// Centraliza a configuração dos serviços de domínio, facilitando a manutenção e a organização das dependências da aplicação.
/// O método <see cref="AddDomainServices(IServiceCollection)"/> registra as implementações necessárias para o funcionamento do domínio,
/// como o serviço <see cref="IMovieService"/>.
/// </remarks>
[ExcludeFromCodeCoverage]
public static class Bootstrap
{
    /// <summary>
    /// Registra os serviços de domínio no container de injeção de dependência.
    /// </summary>
    /// <param name="services">Coleção de serviços da aplicação.</param>
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
    }
}

