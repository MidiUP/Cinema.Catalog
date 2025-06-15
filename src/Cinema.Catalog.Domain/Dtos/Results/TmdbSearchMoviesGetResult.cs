using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Dtos.Results;

/// <summary>
/// Modelo do retorno para a rota /search/movie do TMDb API
/// (https://developers.themoviedb.org/3/search/search-movies)
/// <para>
/// Este modelo representa exatamente o retorno da rota search/movie API
/// TMDb e eh o retorno do metodo <see cref="ITmdbApi.SearchMovies"/>.
/// O Refit implementa a deserializacao do resultado da chamada para esta
/// estrutura.
/// </para>
/// <para>    
/// Note que esta classe eh interna ao Adaptador, 
/// os dados serao mapeados para <see cref="Models.Filme" />
/// para serem expostos.
/// O mapeamento eh feito em <see cref="TmdbAdapter.GetFilmesAsync"/>.
/// </para>
/// </summary>

[ExcludeFromCodeCoverage]
public class TmdbSearchMoviesGetResult
{
    public class ResultItem
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Overview { get; set; } = null!;

        [JsonProperty(PropertyName = "release_date")]
        public DateTimeOffset? ReleaseDate { get; set; }
    }

    public IEnumerable<ResultItem> Results { get; set; } = null!;
}
