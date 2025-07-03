using Cinema.Catalog.Domain.Dtos.Results;
using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Domain.Mappers;
using Cinema.Catalog.Domain.Models;
using Cinema.Catalog.Domain.Shared;
using Cinema.Catalog.Infrastructure.HttpClients;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Cinema.Catalog.Infrastructure.ApiFacades;

public class TmdbApiFacade : ITmdbApiFacade
{
    private readonly HttpClient _httpClient;
    private readonly TmdbApiOptions _tmdbApiOptions;

    public TmdbApiFacade(IHttpClientFactory httpClientFactory, IOptions<TmdbApiOptions> options)
    {
        _tmdbApiOptions = options.Value;
        _httpClient = httpClientFactory.CreateClient(_tmdbApiOptions.Name);
    }

    public async Task<DetailsMovieModel> GetDetailsMovieAsync(int movieId, CancellationToken cancellationToken = default)
    {
        var path = $"movie/{movieId}";
        var queryParams = new Dictionary<string, string>
        {
            ["api_key"] = _tmdbApiOptions.ApiKey,
            ["language"] = _tmdbApiOptions.Language
        };

        // Try catch para corrigir status code errado na resposta da API, devolve erro 404 ao não encontrar filme
        try
        {
            return await _httpClient.GetAsync<DetailsMovieModel>(path, queryParams, cancellationToken);
        }catch(Exception ex)
        {
            if (ex.Message == "Response status code does not indicate success: 404 (Not Found).")
                return null!;
            throw;
        }

    }

    public async Task<IEnumerable<MovieModel>> GetMoviesAsync(SearchMoviesModel searchMoviesModel, CancellationToken cancellationToken = default)
    {
        var path = "search/movie";
        var queryParams = new Dictionary<string, string>
        {
            ["query"] = searchMoviesModel.TermSearch,
            ["api_key"] = _tmdbApiOptions.ApiKey,
            ["language"] = _tmdbApiOptions.Language
        };

        if (searchMoviesModel.PremiereYear > 0)
            queryParams.Add("year", searchMoviesModel.PremiereYear.ToString());

        var resultRequest = await _httpClient.GetAsync<TmdbSearchMoviesGetResult>(path, queryParams, cancellationToken);
        
        var movies = resultRequest.Results;
        return movies.Select(item => item.ToMovieModel());
    }
}
