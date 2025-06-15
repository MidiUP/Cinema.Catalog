using Cinema.Catalog.Domain.Models.Interfaces;
using Cinema.Catalog.Domain.Shared;

namespace Cinema.Catalog.Domain.Models;

public class SearchMoviesModel : IModelValidator
{
    const string MESSAGE_VALIDATION_ERROR = "O termo de pesquisa não pode ser nulo ou vazio.";

    public required string TermSearch { get; set; }
    public int PremiereYear { get; set; }

    public ValidationResult Validation()
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(TermSearch))
            result.AddError(MESSAGE_VALIDATION_ERROR);

        return result;
    }
}
