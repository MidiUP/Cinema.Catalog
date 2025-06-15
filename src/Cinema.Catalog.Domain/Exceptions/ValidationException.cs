using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Cinema.Catalog.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationException : CinemaCatalogException
{
    const string ERROR_EXCEPTION_MESSAGE = "Validation Exception";
    public override int ERROR_CODE => (int)HttpStatusCode.BadRequest;
    public ValidationException(List<string> errors) : base(errors, ERROR_EXCEPTION_MESSAGE) { }
    public ValidationException(string error) : base([error], ERROR_EXCEPTION_MESSAGE) { }
}
