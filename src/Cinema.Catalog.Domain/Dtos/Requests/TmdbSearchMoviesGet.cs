using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Dtos.Requests;

/// <summary>
/// Modelo do entrada para a rota /search/movie do TMDb API
/// (https://developers.themoviedb.org/3/search/search-movies)
/// <para>
/// Este modelo representa exatamente os parametros para requisicoes na
/// rota search/movie API TMDb e
/// eh utilizado como parametro de entrada para o metodo
/// <see cref="ITmdbApi.SearchMovies"/>.
/// </para>
/// <para>    
/// Note que esta classe eh interna ao Adaptador, 
/// os dados serao mapeados a partir de <see cref="Models.Pesquisa" />.
/// O mapeamento eh feito em <see cref="TmdbAdapter.GetFilmesAsync"/>.
/// </para>
/// </summary>

[ExcludeFromCodeCoverage]
public class TmdbSearchMoviesGet
{
    public string Query { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int? Year { get; set; } = null!;
}

[ExcludeFromCodeCoverage]
public class TmdbSearchMoviesGetById
{
    public string Query { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
