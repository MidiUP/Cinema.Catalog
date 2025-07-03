using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Shared;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public static string ENVIRONMENT => Environment.GetEnvironmentVariable("ENV") ?? throw new ConfigurationErrorsException("A variável de amibente ENV não pode ser nula.");
}
