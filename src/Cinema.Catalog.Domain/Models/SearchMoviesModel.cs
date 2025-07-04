using Cinema.Catalog.Domain.Models.Interfaces;
using Cinema.Catalog.Domain.Shared;

namespace Cinema.Catalog.Domain.Models;

/// <summary>
/// Modelo utilizado para representar os parâmetros de busca de filmes na aplicação.
/// </summary>
/// <remarks>
/// Permite a definição do termo de pesquisa e do ano de lançamento para filtrar os resultados.
/// Implementa <see cref="IModelValidator"/> para garantir a validação dos dados antes da execução da busca.
/// </remarks>
public class SearchMoviesModel : IModelValidator
{
    const string MESSAGE_VALIDATION_ERROR = "O termo de pesquisa não pode ser nulo ou vazio.";

    /// <summary>
    /// Termo de pesquisa utilizado para buscar filmes.
    /// </summary>
    public required string TermSearch { get; set; }

    /// <summary>
    /// Ano de lançamento do filme (opcional).
    /// </summary>
    public int PremiereYear { get; set; }

    /// <summary>
    /// Executa a validação do modelo de busca, garantindo que o termo de pesquisa seja informado.
    /// </summary>
    /// <returns>
    /// Um objeto <see cref="ValidationResult"/> indicando se a validação foi bem-sucedida
    /// e contendo eventuais mensagens de erro.
    /// </returns>
    public ValidationResult Validation()
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(TermSearch))
            result.AddError(MESSAGE_VALIDATION_ERROR);

        return result;
    }
}

