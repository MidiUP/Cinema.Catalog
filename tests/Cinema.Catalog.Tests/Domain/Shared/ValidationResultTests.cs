using Cinema.Catalog.Domain.Shared;

namespace Cinema.Catalog.Tests.Domain.Shared;

public class ValidationResultTests
{
    [Fact]
    public void DefaultConstructor_ShouldBeValid_AndHaveNoErrors()
    {
        // Arrange & Act
        var result = new ValidationResult();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Constructor_WithError_ShouldNotBeValid_AndContainError()
    {
        // Arrange
        var error = "Erro de validação";

        // Act
        var result = new ValidationResult(error);

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(error, result.Errors[0]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrWhiteSpaceError_ShouldThrowArgumentException(string? error)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new ValidationResult(error!));
        Assert.Equal("O erro não pode ser nulo ou vazio.", ex.Message);
    }

    [Fact]
    public void AddError_ShouldAddError_AndSetIsValidToFalse()
    {
        // Arrange
        var result = new ValidationResult();

        // Act
        result.AddError("Erro 1");

        // Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("Erro 1", result.Errors[0]);
    }

    [Fact]
    public void AddError_MultipleTimes_ShouldAccumulateErrors()
    {
        // Arrange
        var result = new ValidationResult();

        // Act
        result.AddError("Erro 1");
        result.AddError("Erro 2");

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains("Erro 1", result.Errors);
        Assert.Contains("Erro 2", result.Errors);
    }
}
