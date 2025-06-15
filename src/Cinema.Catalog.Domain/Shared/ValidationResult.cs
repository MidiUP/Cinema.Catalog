using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Shared;

public class ValidationResult
{
    /// <summary>
    /// Indica se a validação foi bem sucedida
    /// </summary>
    public bool IsValid
    {
        get
        {
            return Errors.Count == 0;
        }
    }

    /// <summary>
    /// Lista de mensagens de erro
    /// </summary>
    public List<string> Errors { get; private set; } = new List<string>();

    public ValidationResult() { }

    public ValidationResult(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentException("O erro não pode ser nulo ou vazio.");

        Errors.Add(error);
    }

    /// <summary>
    /// Adiciona uma mensagem a lista de erros.
    /// </summary>
    /// <param name="error"></param>
    public void AddError(string error)
    {
        Errors.Add(error);
    }

}
