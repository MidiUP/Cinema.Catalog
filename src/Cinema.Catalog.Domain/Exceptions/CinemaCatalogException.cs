using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Exceptions;

/// <summary>
/// Classe base abstrata para exceções específicas do domínio do catálogo de cinema.
/// </summary>
/// <remarks>
/// Centraliza o tratamento de exceções customizadas da aplicação, permitindo a padronização
/// de mensagens de erro, códigos de erro e detalhes adicionais. Todas as exceções de domínio
/// devem herdar desta classe para garantir consistência no tratamento e na resposta de erros.
/// </remarks>
[ExcludeFromCodeCoverage]
public abstract class CinemaCatalogException : Exception
{
    /// <summary>
    /// Código de erro específico da exceção.
    /// </summary>
    public abstract int ERROR_CODE { get; }

    /// <summary>
    /// Lista de mensagens de erro detalhadas (opcional).
    /// </summary>
    /// <remarks>
    /// Pode ser utilizada para fornecer múltiplos detalhes sobre os erros ocorridos.
    /// </remarks>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CinemaCatalogException"/> com uma lista de erros e uma mensagem.
    /// </summary>
    /// <param name="errors">Lista de mensagens de erro.</param>
    /// <param name="message">Mensagem geral da exceção.</param>
    public CinemaCatalogException(List<string> errors, string message) : base(message)
    {
        Errors = errors;
    }

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CinemaCatalogException"/> com uma lista de erros e uma mensagem padrão.
    /// </summary>
    /// <param name="errors">Lista de mensagens de erro.</param>
    public CinemaCatalogException(List<string> errors) : this(errors, "Cinema API Gateway Exception") { }
}
