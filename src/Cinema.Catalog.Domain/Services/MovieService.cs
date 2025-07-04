using Cinema.Catalog.Domain.Exceptions;
using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Domain.Models;
using Cinema.Catalog.Domain.Services.Interfaces;

namespace Cinema.Catalog.Domain.Services;

/// <summary>
/// Serviço responsável pela orquestração das operações de consulta de filmes no domínio do catálogo.
/// </summary>
/// <remarks>
/// Implementa a interface <see cref="IMovieService"/>, centralizando a lógica de obtenção de detalhes e listagem de filmes.
/// Realiza a validação dos parâmetros de busca e controla o tempo limite das requisições para a API TMDb.
/// Lança <see cref="ValidationException"/> em caso de parâmetros inválidos.
/// </remarks>
public class MovieService(ITmdbApiFacade tmdbApiFacade) : IMovieService
{
    private readonly ITmdbApiFacade _tmdbApiFacade = tmdbApiFacade;

    private readonly TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Obtém os detalhes completos de um filme a partir do identificador do TMDb.
    /// </summary>
    /// <param name="movieId">Identificador único do filme no The Movie Database (TMDb).</param>
    /// <returns>Um objeto <see cref="DetailsMovieModel"/> com as informações detalhadas do filme.</returns>
    public Task<DetailsMovieModel> GetDetailsMovieAsync(int movieId)
    {
        var cts = new CancellationTokenSource(DEFAULT_TIMEOUT);
        return _tmdbApiFacade.GetDetailsMovieAsync(movieId, cts.Token);
    }

    /// <summary>
    /// Realiza a busca de filmes conforme os critérios informados, validando os parâmetros antes da consulta.
    /// </summary>
    /// <param name="searchMoviesModel">Modelo contendo os parâmetros de busca, como termo e ano de lançamento.</param>
    /// <returns>Uma coleção de <see cref="MovieModel"/> representando os filmes encontrados.</returns>
    /// <exception cref="ValidationException">Lançada quando os parâmetros de busca são inválidos.</exception>
    public Task<IEnumerable<MovieModel>> GetMoviesAsync(SearchMoviesModel searchMoviesModel)
    {
        var validationSearchMoviesModel = searchMoviesModel.Validation();
        if (!validationSearchMoviesModel.IsValid)
            throw new ValidationException(validationSearchMoviesModel.Errors);

        var cts = new CancellationTokenSource(DEFAULT_TIMEOUT);
        return _tmdbApiFacade.GetMoviesAsync(searchMoviesModel, cts.Token);
    }
}

