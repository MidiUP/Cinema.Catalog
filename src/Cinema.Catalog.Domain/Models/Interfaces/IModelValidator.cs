using Cinema.Catalog.Domain.Shared;

namespace Cinema.Catalog.Domain.Models.Interfaces;

public interface IModelValidator
{
    public ValidationResult Validation();
}
