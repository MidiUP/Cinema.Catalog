using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Dtos.Requests;

/// <summary>
/// Modelo de entrada para a rota <c>/search/movie</c> da API TMDb.
/// <para>
/// Representa os parâmetros aceitos pela rota <c>search/movie</c> da TMDb API,
/// conforme documentação oficial: https://developers.themoviedb.org/3/search/search-movies.
/// </para>
/// <para>
/// Utilizado como parâmetro de entrada no método <see cref="ITmdbApi.SearchMovies"/>.
/// </para>
/// <para>
/// Esta classe é interna ao adaptador; os dados são mapeados a partir de <see cref="Models.Pesquisa"/>.
/// O mapeamento ocorre em <see cref="TmdbAdapter.GetFilmesAsync"/>.
/// </para>
/// </summary>
[ExcludeFromCodeCoverage]
public class TmdbSearchMoviesGet
{
    /// <summary>
    /// Texto da busca.
    /// </summary>
    public string Query { get; set; } = null!;

    /// <summary>
    /// Chave de API do TMDb.
    /// </summary>
    public string ApiKey { get; set; } = null!;

    /// <summary>
    /// Idioma dos resultados (ex: "pt-BR").
    /// </summary>
    public string Language { get; set; } = null!;

    /// <summary>
    /// Ano de lançamento para filtrar os resultados.
    /// </summary>
    public int? Year { get; set; } = null!;
}
