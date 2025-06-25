using Cinema.Catalog.Domain.Models;
using Cinema.Catalog.Integration.Tests.Config;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Cinema.Catalog.Integration.Tests.Controllers.V1;

[TestFixture]
public class CatalogControllerTests : CinemaCatalogWebApplicationFactory
{
    [Test]
    public async Task GetMoviesAsync_DeveRetornar200ComListaDeFilmes()
    {
        // Arrange
        var query = "?TermSearch=Matrix&PremiereYear=1999";

        // Act
        var response = await Client!.GetAsync($"/api/v1/movies{query}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var movies = await response.Content.ReadFromJsonAsync<IEnumerable<MovieModel>>();
        movies.Should().NotBeNull();
        movies.Should().NotBeEmpty();
    }

    [Test]
    public async Task GetMoviesAsync_DeveRetornar204QuandoNaoHouverFilmes()
    {
        // Arrange
        var query = "?TermSearch=FilmeInexistente&PremiereYear=1900";

        // Act
        var response = await Client!.GetAsync($"/api/v1/movies{query}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Test]
    public async Task GetMoviesAsync_DeveRetornar400QuandoNaoPassarParametros()
    {
        // Arrange
        var query = "";

        // Act
        var response = await Client!.GetAsync($"/api/v1/movies{query}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task GetMoviesByIdAsync_DeveRetornar200ComDetalhesDoFilme()
    {
        // Arrange
        var movieId = 1;

        // Act
        var response = await Client!.GetAsync($"/api/v1/movies/{movieId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var details = await response.Content.ReadFromJsonAsync<DetailsMovieModel>();
        details.Should().NotBeNull();
    }

    [Test]
    public async Task GetMoviesByIdAsync_DeveRetornar204QuandoFilmeNaoExiste()
    {
        // Arrange
        var movieId = 999999;

        // Act
        var response = await Client!.GetAsync($"/api/v1/movies/{movieId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
