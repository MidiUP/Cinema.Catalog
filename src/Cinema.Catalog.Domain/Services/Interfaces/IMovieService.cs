using Cinema.Catalog.Domain.Models;

namespace Cinema.Catalog.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface de serviço responsável pelas operações de consulta de filmes no domínio do catálogo.
    /// </summary>
    /// <remarks>
    /// Centraliza a lógica de obtenção de detalhes e listagem de filmes, abstraindo a fonte dos dados
    /// (por exemplo, integrações externas ou banco de dados interno). Facilita a manutenção, testes e evolução
    /// das regras de negócio relacionadas à busca de filmes.
    /// </remarks>
    public interface IMovieService
    {
        /// <summary>
        /// Obtém os detalhes completos de um filme a partir do identificador do TMDb.
        /// </summary>
        /// <param name="movieId">Identificador único do filme no The Movie Database (TMDb).</param>
        /// <returns>Um objeto <see cref="DetailsMovieModel"/> com as informações detalhadas do filme.</returns>
        Task<DetailsMovieModel> GetDetailsMovieAsync(int movieId);

        /// <summary>
        /// Realiza a busca de filmes conforme os critérios informados.
        /// </summary>
        /// <param name="searchMoviesModel">Modelo contendo os parâmetros de busca, como termo e ano de lançamento.</param>
        /// <returns>Uma coleção de <see cref="MovieModel"/> representando os filmes encontrados.</returns>
        Task<IEnumerable<MovieModel>> GetMoviesAsync(SearchMoviesModel searchMoviesModel);
    }
}

