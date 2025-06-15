using Cinema.Catalog.Domain.Dtos.Results;
using Cinema.Catalog.Domain.Mappers;

namespace Cinema.Catalog.Tests.Domain.Mappers;

public class MovieMappersTests
{
    [Fact]
    public void ToMovieModel_Maps_All_Properties_Correctly()
    {
        // Arrange
        var resultItem = new TmdbSearchMoviesGetResult.ResultItem
        {
            Id = 123,
            Title = "Filme Teste",
            Overview = "Descrição do filme",
            ReleaseDate = new DateTimeOffset(2024, 6, 15, 0, 0, 0, TimeSpan.Zero)
        };

        // Act
        var movieModel = resultItem.ToMovieModel();

        // Assert
        Assert.Equal(resultItem.Id, movieModel.Id);
        Assert.Equal(resultItem.Title, movieModel.Name);
        Assert.Equal(resultItem.Overview, movieModel.Description);
        Assert.Equal(resultItem.ReleaseDate, movieModel.PremiereYear);
    }

    [Fact]
    public void ToMovieModel_Handles_Null_ReleaseDate()
    {
        // Arrange
        var resultItem = new TmdbSearchMoviesGetResult.ResultItem
        {
            Id = 456,
            Title = "Sem Data",
            Overview = "Sem data de lançamento",
            ReleaseDate = null
        };

        // Act
        var movieModel = resultItem.ToMovieModel();

        // Assert
        Assert.Equal(resultItem.Id, movieModel.Id);
        Assert.Equal(resultItem.Title, movieModel.Name);
        Assert.Equal(resultItem.Overview, movieModel.Description);
        Assert.Null(movieModel.PremiereYear);
    }

    [Fact]
    public void ToMovieModel_Handles_Empty_Strings()
    {
        // Arrange
        var resultItem = new TmdbSearchMoviesGetResult.ResultItem
        {
            Id = 0,
            Title = string.Empty,
            Overview = string.Empty,
            ReleaseDate = null
        };

        // Act
        var movieModel = resultItem.ToMovieModel();

        // Assert
        Assert.Equal(0, movieModel.Id);
        Assert.Equal(string.Empty, movieModel.Name);
        Assert.Equal(string.Empty, movieModel.Description);
        Assert.Null(movieModel.PremiereYear);
    }
}
