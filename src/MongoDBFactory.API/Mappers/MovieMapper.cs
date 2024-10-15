using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Mappers;

namespace MongoDBFactory.API.Mappers;

public sealed class MovieMapper(IDirectorMapper directorMapper) : IMovieMapper
{
    public Movie CreateRequestToDomain(CreateMovieRequest createMovieRequest) =>
        new()
        {
            Title = createMovieRequest.Title,
            Genre = createMovieRequest.Genre,
            ReleaseYear = createMovieRequest.ReleaseYear,
            Director = directorMapper.RequestToDomain(createMovieRequest.Director)
        };

    public MovieResponse DomainToResponse(Movie movie) =>
        new(movie.Id,
            movie.Title,
            movie.Genre,
            movie.ReleaseYear,
            directorMapper.DomainToResponse(movie.Director));

    public Movie UpdateRequestToDomain(UpdateMovieRequest updateMovieRequest) =>
        new()
        {
            Id = updateMovieRequest.Id,
            Title = updateMovieRequest.Title,
            Genre = updateMovieRequest.Genre,
            ReleaseYear = updateMovieRequest.ReleaseYear,
            Director = directorMapper.RequestToDomain(updateMovieRequest.Director)
        };
}
