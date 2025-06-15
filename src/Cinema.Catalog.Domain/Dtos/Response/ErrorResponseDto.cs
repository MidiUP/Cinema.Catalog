using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Dtos.Response;

[ExcludeFromCodeCoverage]
public record ErrorResponseDto
{
    public List<string>? Errors { get; set; }
    public string? Message { get; set; }

    public ErrorResponseDto(List<string> errors, string message)
    {
        Errors = errors;
        Message = message;
    }
    
    public ErrorResponseDto(string error, string message) : this([error], message) { }

    public ErrorResponseDto(string message)
    {
        Message = message;
    }
}
