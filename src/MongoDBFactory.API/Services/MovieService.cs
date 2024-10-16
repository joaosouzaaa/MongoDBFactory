using FluentValidation;
using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Mappers;
using MongoDBFactory.API.Interfaces.Repositories;
using MongoDBFactory.API.Interfaces.Services;
using MongoDBFactory.API.Interfaces.Settings;
using MongoDBFactory.API.Settings.PaginationSettings;

namespace MongoDBFactory.API.Services;

public sealed class MovieService(
    IMovieRepository movieRepository,
    IMovieMapper movieMapper,
    IValidator<Movie> validator,
    INotificationHandler notificationHandler)
    : IMovieService
{
    public async Task AddAsync(CreateMovieRequest createMovieRequest, CancellationToken cancellationToken)
    {
        var movie = movieMapper.CreateRequestToDomain(createMovieRequest);

        if (!await IsValidAsync(movie, cancellationToken))
        {
            return;
        }

        await movieRepository.AddAsync(movie, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await movieRepository.AnyAsync(id, cancellationToken))
        {
            notificationHandler.AddNotification("NotFound", $"Movie with Id {id} not found.");

            return;
        }

        await movieRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<List<MovieResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var movieList = await movieRepository.GetAllAsync(cancellationToken);

        return movieMapper.DomainListToResponseList(movieList);
    }

    public async Task<PageList<MovieResponse>> GetAllPaginatedAsync(PageParameters parameters, CancellationToken cancellationToken)
    {
        var moviePageList = await movieRepository.GetAllPaginatedAsync(parameters, cancellationToken);

        return movieMapper.DomainPageListToResponsePageList(moviePageList);
    }

    public async Task<MovieResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var movie = await movieRepository.GetByIdAsync(id, cancellationToken);

        if (movie is null)
        {
            return null;
        }

        return movieMapper.DomainToResponse(movie);
    }

    public async Task UpdateAsync(UpdateMovieRequest updateMovieRequest, CancellationToken cancellationToken)
    {
        if (!await movieRepository.AnyAsync(updateMovieRequest.Id, cancellationToken))
        {
            notificationHandler.AddNotification("NotFound", $"Movie with Id {updateMovieRequest.Id} not found.");

            return;
        }

        var movie = movieMapper.UpdateRequestToDomain(updateMovieRequest);

        if (!await IsValidAsync(movie, cancellationToken))
        {
            return;
        }

        await movieRepository.UpdateAsync(movie, cancellationToken);
    }

    private async Task<bool> IsValidAsync(Movie movie, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(movie, cancellationToken);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
