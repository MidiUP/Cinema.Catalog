using Cinema.Catalog.Domain.Shared;

namespace Cinema.Catalog.Domain.Models.Interfaces;

/// <summary>
/// Interface para validação de modelos de domínio.
/// </summary>
/// <remarks>
/// Define um contrato para que modelos implementem lógica de validação própria,
/// retornando um <see cref="ValidationResult"/> com o status e os detalhes da validação.
/// Utilizada para garantir a integridade dos dados antes de operações críticas,
/// como persistência ou processamento de regras de negócio.
/// </remarks>
public interface IModelValidator
{
    /// <summary>
    /// Executa a validação do modelo e retorna o resultado.
    /// </summary>
    /// <returns>
    /// Um objeto <see cref="ValidationResult"/> indicando se a validação foi bem-sucedida
    /// e contendo eventuais mensagens de erro.
    /// </returns>
    public ValidationResult Validation();
}

