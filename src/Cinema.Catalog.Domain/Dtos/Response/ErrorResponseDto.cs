using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Dtos.Response;

/// <summary>
/// DTO responsável por padronizar as respostas de erro da API.
/// </summary>
/// <remarks>
/// Centraliza o retorno de mensagens de erro e listas de erros detalhados em respostas HTTP,
/// facilitando o tratamento de falhas pelos clientes da aplicação.
/// Pode ser utilizado em diferentes cenários de erro, como validação, exceções de negócio ou falhas inesperadas.
/// </remarks>
[ExcludeFromCodeCoverage]
public record ErrorResponseDto
{
    /// <summary>
    /// Lista de mensagens de erro detalhadas (opcional).
    /// </summary>
    /// <remarks>
    /// Utilizada para fornecer múltiplos detalhes sobre os erros ocorridos em uma requisição.
    /// </remarks>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Mensagem geral de erro (opcional).
    /// </summary>
    /// <remarks>
    /// Pode ser utilizada para exibir uma mensagem amigável ou genérica ao usuário final.
    /// </remarks>
    public string? Message { get; set; }

    /// <summary>
    /// Cria uma instância de <see cref="ErrorResponseDto"/> com uma lista de erros e uma mensagem.
    /// </summary>
    /// <param name="errors">Lista de mensagens de erro.</param>
    /// <param name="message">Mensagem geral de erro.</param>
    public ErrorResponseDto(List<string> errors, string message)
    {
        Errors = errors;
        Message = message;
    }

    /// <summary>
    /// Cria uma instância de <see cref="ErrorResponseDto"/> com um erro único e uma mensagem.
    /// </summary>
    /// <param name="error">Mensagem de erro única.</param>
    /// <param name="message">Mensagem geral de erro.</param>
    public ErrorResponseDto(string error, string message) : this([error], message) { }

    /// <summary>
    /// Cria uma instância de <see cref="ErrorResponseDto"/> apenas com uma mensagem.
    /// </summary>
    /// <param name="message">Mensagem geral de erro.</param>
    public ErrorResponseDto(string message)
    {
        Message = message;
    }
}
