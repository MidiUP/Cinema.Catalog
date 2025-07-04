using Cinema.Catalog.Domain.Models;

namespace Cinema.Catalog.Domain.Infrastructure.ApiFacades;

/// <summary>
/// Interface de fachada para integração com a API do TMDb (The Movie Database).
/// </summary>
/// <remarks>
/// Centraliza as operações de consulta de filmes e detalhes de filmes na API TMDb,
/// abstraindo a implementação e facilitando a manutenção e testes.
/// </remarks>
public interface ITmdbApiFacade
{
    /// <summary>
    /// Obtém os detalhes completos de um filme a partir do identificador do TMDb.
    /// </summary>
    /// <param name="movieId">Identificador único do filme no TMDb.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação (opcional).</param>
    /// <returns>Um objeto <see cref="DetailsMovieModel"/> com as informações detalhadas do filme.</returns>
    Task<DetailsMovieModel> GetDetailsMovieAsync(int movieId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Realiza a busca de filmes no TMDb conforme os critérios informados.
    /// </summary>
    /// <param name="searchMoviesModel">Modelo contendo os parâmetros de busca, como termo e ano de lançamento.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação (opcional).</param>
    /// <returns>Uma coleção de <see cref="MovieModel"/> representando os filmes encontrados.</returns>
    Task<IEnumerable<MovieModel>> GetMoviesAsync(SearchMoviesModel searchMoviesModel, CancellationToken cancellationToken = default);
}

