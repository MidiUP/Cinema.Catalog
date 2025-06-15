using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.Domain.Shared;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public static string ENVIRONMENT => Environment.GetEnvironmentVariable("ENV") ?? throw new ConfigurationErrorsException("A variável de amibente ENV não pode ser nula.");
    
    public static class TmdbApi
    {
        public static string NAME => "TmdbApi";
        public static string LANGUAGE => "pt-BR";
        public static string API_KEY => Environment.GetEnvironmentVariable("TMDB_API_KEY") ?? throw new ConfigurationErrorsException("A variável de amibente TMDB_API_KEY não pode ser nula.");
        public static string BASE_URL => Environment.GetEnvironmentVariable("TMDB_API_BASE_URL") ?? throw new ConfigurationErrorsException("A variável de amibente TMDB_API_BASE_URL não pode ser nula.");
        public static string AUTH_TOKEN => Environment.GetEnvironmentVariable("TMDB_API_AUTH_TOKEN") ?? throw new ConfigurationErrorsException("A variável de amibente TMDB_API_AUTH_TOKEN não pode ser nula.");
    }
}
