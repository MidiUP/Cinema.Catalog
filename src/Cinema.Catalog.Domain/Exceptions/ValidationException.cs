using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Cinema.Catalog.Domain.Exceptions;

/// <summary>
/// Exceção de domínio lançada quando ocorre uma falha de validação em operações do catálogo de cinema.
/// </summary>
/// <remarks>
/// Utilizada para sinalizar erros de validação de dados de entrada, como parâmetros inválidos ou inconsistentes.
/// Retorna o código de erro HTTP 400 (Bad Request), permitindo padronização no tratamento e resposta de erros.
/// Herda de <see cref="CinemaCatalogException"/>, garantindo consistência no tratamento de exceções do domínio.
/// </remarks>
[ExcludeFromCodeCoverage]
public class ValidationException : CinemaCatalogException
{
    const string ERROR_EXCEPTION_MESSAGE = "Validation Exception";

    /// <summary>
    /// Código de erro específico para falhas de validação, baseado em <see cref="HttpStatusCode.BadRequest"/>.
    /// </summary>
    public override int ERROR_CODE => (int)HttpStatusCode.BadRequest;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ValidationException"/> com uma lista de erros de validação.
    /// </summary>
    /// <param name="errors">Lista de mensagens de erro de validação.</param>
    public ValidationException(List<string> errors) : base(errors, ERROR_EXCEPTION_MESSAGE) { }

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ValidationException"/> com uma mensagem de erro de validação única.
    /// </summary>
    /// <param name="error">Mensagem de erro de validação.</param>
    public ValidationException(string error) : base([error], ERROR_EXCEPTION_MESSAGE) { }
}

