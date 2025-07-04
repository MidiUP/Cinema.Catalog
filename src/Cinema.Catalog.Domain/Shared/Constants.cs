using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Shared;

/// <summary>
/// Classe utilitária que centraliza constantes e variáveis globais do domínio.
/// </summary>
/// <remarks>
/// Fornece acesso padronizado a valores globais utilizados em diferentes partes da aplicação.
/// Atualmente, expõe a variável de ambiente <c>ENV</c>, que define o ambiente de execução (ex: Development, Production, Test).
/// Caso a variável não esteja definida, uma exceção de configuração é lançada para evitar comportamentos inesperados.
/// </remarks>
[ExcludeFromCodeCoverage]
public static class Constants
{
    /// <summary>
    /// Obtém o valor da variável de ambiente <c>ENV</c> que indica o ambiente de execução da aplicação.
    /// </summary>
    /// <remarks>
    /// Lança <see cref="ConfigurationErrorsException"/> se a variável não estiver definida.
    /// </remarks>
    public static string ENVIRONMENT => Environment.GetEnvironmentVariable("ENV") ?? throw new ConfigurationErrorsException("A variável de amibente ENV não pode ser nula.");
}

