using Cinema.Catalog.Domain.Dtos.Results;
using Cinema.Catalog.Domain.Models;
using Cinema.Catalog.Infrastructure.ApiFacades;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace Cinema.Catalog.Tests.API.Infrastructure.ApiFacades;

public class TmdbApiFacadeTests
{
    public TmdbApiFacadeTests()
    {
        Environment.SetEnvironmentVariable("TMDB_API_KEY", "any_key");
        Environment.SetEnvironmentVariable("TMDB_API_BASE_URL", "any_url");
        Environment.SetEnvironmentVariable("TMDB_API_AUTH_TOKEN", "any_token");
    }

    private static HttpClient CreateHttpClient(HttpResponseMessage response)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response)
            .Verifiable();

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.themoviedb.org/3/")
        };
    }

    private static IHttpClientFactory CreateFactory(HttpClient client)
    {
        var factoryMock = new Mock<IHttpClientFactory>();
        factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);
        return factoryMock.Object;
    }

    [Fact]
    public async Task GetDetailsMovieAsync_ReturnsModel_OnSuccess()
    {
        // Arrange
        var details = new DetailsMovieModel { Id = 1, Title = "Filme" };
        var json = System.Text.Json.JsonSerializer.Serialize(details);
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
        var client = CreateHttpClient(response);
        var factory = CreateFactory(client);
        var facade = new TmdbApiFacade(factory);

        // Act
        var result = await facade.GetDetailsMovieAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(details.Id, result.Id);
        Assert.Equal(details.Title, result.Title);
    }

    [Fact]
    public async Task GetDetailsMovieAsync_ReturnsNull_On404()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("", System.Text.Encoding.UTF8, "application/json")
        };
        var client = CreateHttpClient(response);
        var factory = CreateFactory(client);

        // O HttpAdapter provavelmente lança uma exceção com a mensagem específica de 404
        // Simulando isso:
        var facade = new TmdbApiFacade(factory);

        // Para simular o comportamento do HttpAdapter, você pode mockar o método GetAsync se ele for virtual ou usar um wrapper.
        // Aqui, assumimos que a exceção será lançada e capturada pelo try/catch do método.

        // Act
        var result = await facade.GetDetailsMovieAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetMoviesAsync_ReturnsMappedMovies()
    {
        // Arrange
        var tmdbResult = new TmdbSearchMoviesGetResult
        {
            Results =
            [
                new() {
                    Id = 1,
                    Title = "Filme 1",
                    Overview = "Desc 1",
                    ReleaseDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                new() {
                    Id = 2,
                    Title = "Filme 2",
                    Overview = "Desc 2",
                    ReleaseDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
                }
            ]
        };
        var json = JsonConvert.SerializeObject(tmdbResult);
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
        var client = CreateHttpClient(response);
        var factory = CreateFactory(client);
        var facade = new TmdbApiFacade(factory);

        var searchModel = new SearchMoviesModel { TermSearch = "Filme", PremiereYear = 0 };

        // Act
        var result = await facade.GetMoviesAsync(searchModel);

        // Assert
        Assert.NotNull(result);
        var list = Assert.IsAssignableFrom<IEnumerable<MovieModel>>(result);
        Assert.Collection(list,
            m =>
            {
                Assert.Equal(1, m.Id);
                Assert.Equal("Filme 1", m.Name);
                Assert.Equal("Desc 1", m.Description);
                Assert.Equal(new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero), m.PremiereYear);
            },
            m =>
            {
                Assert.Equal(2, m.Id);
                Assert.Equal("Filme 2", m.Name);
                Assert.Equal("Desc 2", m.Description);
                Assert.Equal(new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero), m.PremiereYear);
            }
        );
    }
}
