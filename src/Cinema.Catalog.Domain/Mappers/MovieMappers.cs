using Cinema.Catalog.Domain.Dtos.Results;
using Cinema.Catalog.Domain.Models;

namespace Cinema.Catalog.Domain.Mappers;

/// <summary>
/// Classe utilitária responsável por realizar o mapeamento entre modelos de dados do TMDb e modelos internos da aplicação.
/// </summary>
/// <remarks>
/// Centraliza a conversão de objetos retornados pela API TMDb para o modelo <see cref="MovieModel"/> utilizado internamente.
/// Facilita a manutenção e padroniza o processo de transformação de dados entre camadas.
/// </remarks>
public static class MovieMappers
{
    /// <summary>
    /// Converte um item de resultado da busca de filmes do TMDb em um <see cref="MovieModel"/>.
    /// </summary>
    /// <param name="item">Instância de <see cref="TmdbSearchMoviesGetResult.ResultItem"/> a ser convertida.</param>
    /// <returns>Um objeto <see cref="MovieModel"/> preenchido com os dados do item informado.</returns>
    public static MovieModel ToMovieModel(this TmdbSearchMoviesGetResult.ResultItem item)
    {
        return new MovieModel
        {
            Id = item.Id,
            Description = item.Overview,
            Name = item.Title,
            PremiereYear = item.ReleaseDate
        };
    }
}

