namespace Cinema.Catalog.Domain.Models;

/// <summary>
/// Modelo simplificado que representa um filme retornado pela API TMDb.
/// </summary>
/// <remarks>
/// Utilizado para exibir informações resumidas de filmes em listagens ou buscas.
/// Contém apenas os principais dados necessários para identificação e apresentação do filme.
/// </remarks>
public class MovieModel
{
    /// <summary>
    /// Identificador do filme no The Movie Database (TMDb).
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Nome ou título do filme.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Descrição ou resumo do filme.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Data de lançamento do filme.
    /// </summary>
    public DateTimeOffset? PremiereYear { get; set; }
}

