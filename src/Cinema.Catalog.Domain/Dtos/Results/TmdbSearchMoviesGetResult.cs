using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Dtos.Results;

/// <summary>
/// Modelo de retorno para a rota <c>/search/movie</c> da API TMDb.
/// </summary>
/// <remarks>
/// Representa exatamente o formato de resposta da rota <c>search/movie</c> da TMDb API,
/// conforme a documentação oficial: https://developers.themoviedb.org/3/search/search-movies.
/// <para>
/// Utilizado como retorno do método <see cref="ITmdbApi.SearchMovies"/>.
/// A deserialização do resultado é feita automaticamente pelo Refit para esta estrutura.
/// </para>
/// <para>
/// Esta classe é interna ao adaptador; os dados são mapeados para <see cref="Models.Filme"/>
/// para exposição externa. O mapeamento ocorre em <see cref="TmdbAdapter.GetFilmesAsync"/>.
/// </para>
/// </remarks>
[ExcludeFromCodeCoverage]
public class TmdbSearchMoviesGetResult
{
    /// <summary>
    /// Representa um item individual do resultado da busca de filmes.
    /// </summary>
    /// <remarks>
    /// Cada instância corresponde a um filme retornado pela busca na API TMDb.
    /// </remarks>
    public class ResultItem
    {
        /// <summary>
        /// Identificador único do filme no TMDb.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Título do filme.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Sinopse ou descrição do filme.
        /// </summary>
        public string Overview { get; set; } = null!;

        /// <summary>
        /// Data de lançamento do filme (pode ser nula).
        /// </summary>
        [JsonProperty(PropertyName = "release_date")]
        public DateTimeOffset? ReleaseDate { get; set; }
    }

    /// <summary>
    /// Lista de filmes retornados pela busca.
    /// </summary>
    /// <remarks>
    /// Cada item da lista representa um filme encontrado conforme os critérios de busca informados.
    /// </remarks>
    public IEnumerable<ResultItem> Results { get; set; } = null!;
}
