using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Cinema.Catalog.Domain.Exceptions;

/// <summary>
/// Exceção de domínio lançada quando ocorre um timeout em operações do catálogo de cinema.
/// </summary>
/// <remarks>
/// Representa uma falha por tempo excedido em requisições ou operações internas da aplicação.
/// Utiliza o código de erro HTTP 408 (Request Timeout) para padronizar o tratamento e a resposta.
/// Herda de <see cref="CinemaCatalogException"/>, garantindo consistência no tratamento de exceções do domínio.
/// </remarks>
[ExcludeFromCodeCoverage]
public class TimeoutException : CinemaCatalogException
{
    const string ERROR_EXCEPTION_MESSAGE = "Timeout Exception";

    /// <summary>
    /// Código de erro específico para timeout, baseado em <see cref="HttpStatusCode.RequestTimeout"/>.
    /// </summary>
    public override int ERROR_CODE => (int)HttpStatusCode.RequestTimeout;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="TimeoutException"/> com mensagem padrão de timeout.
    /// </summary>
    public TimeoutException() : base([], ERROR_EXCEPTION_MESSAGE) { }
}
