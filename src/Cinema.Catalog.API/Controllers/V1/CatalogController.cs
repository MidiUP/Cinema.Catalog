using Cinema.Catalog.API.Attributes;
using Cinema.Catalog.Domain.Dtos.Response;
using Cinema.Catalog.Domain.Models;
using Cinema.Catalog.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Cinema.Catalog.API.Controllers.V1;

[ExcludeFromCodeCoverage]
[ApiVersion("1")]
[CinemaCatalogVersionedRoute("1")]
public class MoviesController(IMovieService movieService) : CinemaCatalogControllerBase
{
    private readonly IMovieService _movieService = movieService;

    [HttpGet()]
    [ProducesResponseType<IEnumerable<MovieModel>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponseDto>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponseDto>(StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType<ErrorResponseDto>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetMoviesAsync([FromQuery] SearchMoviesModel searchMoviesModel)
    {
        var movies = await _movieService.GetMoviesAsync(searchMoviesModel);

        if (movies is null)
            return NoContent();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<IEnumerable<DetailsMovieModel>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponseDto>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponseDto>(StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType<ErrorResponseDto>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetMoviesAsync([FromRoute] int id)
    {
        var movie = await _movieService.GetDetailsMovieAsync(id);

        if (movie is null)
            return NoContent();

        return Ok(movie);
    }
}
