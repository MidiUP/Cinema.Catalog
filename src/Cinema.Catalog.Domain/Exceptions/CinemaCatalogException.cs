using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class CinemaCatalogException : Exception
{
    public abstract int ERROR_CODE { get; }
    public List<string>? Errors { get; set; }

    public CinemaCatalogException(List<string> errors, string message) : base(message)
    {
        Errors = errors;
    }

    public CinemaCatalogException(List<string> errors) : this(errors, "Cinema API Gateway Exception") { }

}
