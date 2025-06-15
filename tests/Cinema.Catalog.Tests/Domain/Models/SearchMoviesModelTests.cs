using Cinema.Catalog.Domain.Models;

namespace Cinema.Catalog.Tests.Domain.Models;

public class SearchMoviesModelTests
{
    [Fact]
    public void Validation_ReturnsValid_WhenTermSearchIsNotEmpty()
    {
        // Arrange
        var model = new SearchMoviesModel
        {
            TermSearch = "Matrix",
            PremiereYear = 1999
        };

        // Act
        var result = model.Validation();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validation_ReturnsInvalid_WhenTermSearchIsNullOrWhiteSpace(string? term)
    {
        // Arrange
        var model = new SearchMoviesModel
        {
            TermSearch = term!,
            PremiereYear = 2024
        };

        // Act
        var result = model.Validation();

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("O termo de pesquisa não pode ser nulo ou vazio.", result.Errors[0]);
    }
}
