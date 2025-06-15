using Cinema.Catalog.API.Filters;
using Cinema.Catalog.Domain.Dtos.Response;
using Cinema.Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Moq;
namespace Cinema.Catalog.Tests.API.Filters;

public class ExceptionFilterTests
{
    private readonly Mock<ILogger<ExceptionFilter>> _loggerMock = new();

    private ExceptionContext CreateContext(Exception ex)
    {
        var actionContext = new ActionContext
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
        };
        return new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = ex
        };
    }

    [Fact]
    public void OnException_CinemaCatalogException_ReturnsExpectedResult()
    {
        // Arrange
        var ex = new ValidationException("mensagem");
        var context = CreateContext(ex);
        var filter = new ExceptionFilter(_loggerMock.Object);

        // Act
        filter.OnException(context);

        // Assert
        var result = Assert.IsType<ObjectResult>(context.Result);
        Assert.Equal(400, context.HttpContext.Response.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponseDto>(result.Value);
        Assert.Equal("Validation Exception", errorResponse.Message);
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Erro conhecido")),
                ex,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void OnException_OperationCanceledException_ReturnsTimeout()
    {
        // Arrange
        var ex = new OperationCanceledException("timeout");
        var context = CreateContext(ex);
        var filter = new ExceptionFilter(_loggerMock.Object);

        // Act
        filter.OnException(context);

        // Assert
        var result = Assert.IsType<ObjectResult>(context.Result);
        Assert.Equal(408, context.HttpContext.Response.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponseDto>(result.Value);
        Assert.Equal("Sua requisição excedeu o tempo limite.", errorResponse.Message);
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Timeout")),
                ex,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void OnException_UnknownException_ReturnsInternalServerError()
    {
        // Arrange
        var ex = new Exception("erro desconhecido");
        var context = CreateContext(ex);
        var filter = new ExceptionFilter(_loggerMock.Object);

        // Act
        filter.OnException(context);

        // Assert
        var result = Assert.IsType<ObjectResult>(context.Result);
        Assert.Equal(500, context.HttpContext.Response.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponseDto>(result.Value);
        Assert.Equal("Ocorreu um inesperado. Por favor tente novamente mais tarde", errorResponse.Message);
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Erro desconhecido")),
                ex,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
