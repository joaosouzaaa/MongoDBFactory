using Microsoft.AspNetCore.Mvc;
using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Interfaces.Services;
using MongoDBFactory.API.Settings.NotificationSettings;
using MongoDBFactory.API.Settings.PaginationSettings;
using System.Net.Mime;

namespace MongoDBFactory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public sealed class MovieController(IMovieService movieService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task AddAsync([FromBody] CreateMovieRequest createMovieRequest, CancellationToken cancellationToken) =>
        movieService.AddAsync(createMovieRequest, cancellationToken);

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        movieService.DeleteAsync(id, cancellationToken);

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<MovieResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        movieService.GetAllAsync(cancellationToken);

    [HttpGet("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageList<MovieResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PageList<MovieResponse>> GetAllPaginatedAsync([FromQuery] PageParameters pageParameters, CancellationToken cancellationToken) =>
       movieService.GetAllPaginatedAsync(pageParameters, cancellationToken);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<MovieResponse?> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
       movieService.GetByIdAsync(id, cancellationToken);

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task UpdateAsync([FromBody] UpdateMovieRequest updateMovieRequest, CancellationToken cancellationToken) =>
        movieService.UpdateAsync(updateMovieRequest, cancellationToken);
}
