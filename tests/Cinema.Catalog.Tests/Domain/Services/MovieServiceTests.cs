using AutoFixture;
using Cinema.Catalog.Domain.Exceptions;
using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Domain.Models;
using Cinema.Catalog.Domain.Services;
using Moq;

namespace Cinema.Catalog.Tests.Domain.Services;

public class MovieServiceTests
{
    private readonly Mock<ITmdbApiFacade> _tmdbApiFacadeMock;
    private readonly MovieService _service;
    private readonly Fixture _fixture = new Fixture();

    public MovieServiceTests()
    {
        _tmdbApiFacadeMock = new Mock<ITmdbApiFacade>();
        _service = new MovieService(_tmdbApiFacadeMock.Object);
    }

    [Fact]
    public async Task GetDetailsMovieAsync_CallsFacadeAndReturnsResult()
    {
        // Arrange
        var movieId = 42;
        var expected = _fixture.Create<DetailsMovieModel>();
        _tmdbApiFacadeMock
            .Setup(f => f.GetDetailsMovieAsync(movieId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _service.GetDetailsMovieAsync(movieId);

        // Assert
        Assert.Equal(expected, result);
        _tmdbApiFacadeMock.Verify(f => f.GetDetailsMovieAsync(movieId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetMoviesAsync_ValidModel_CallsFacadeAndReturnsResult()
    {
        // Arrange
        var searchModel = new SearchMoviesModel { TermSearch = "Matrix", PremiereYear = 1999 };
        var expected = new List<MovieModel> { new() { Id = 1, Name = "Matrix", Description = "desc" } };
        _tmdbApiFacadeMock
            .Setup(f => f.GetMoviesAsync(searchModel, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        // Act
        var result = await _service.GetMoviesAsync(searchModel);

        // Assert
        Assert.Equal(expected, result);
        _tmdbApiFacadeMock.Verify(f => f.GetMoviesAsync(searchModel, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetMoviesAsync_InvalidModel_ThrowsValidationException()
    {
        // Arrange
        var searchModel = new SearchMoviesModel { TermSearch = "", PremiereYear = 2024 };

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(() => _service.GetMoviesAsync(searchModel));
        Assert.Contains("O termo de pesquisa não pode ser nulo ou vazio.", ex.Errors!);
        _tmdbApiFacadeMock.Verify(f => f.GetMoviesAsync(It.IsAny<SearchMoviesModel>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
