using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Mappers;
using MongoDBFactory.API.Settings.PaginationSettings;

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

    public List<MovieResponse> DomainListToResponseList(List<Movie> movieList) =>
        movieList.Select(DomainToResponse).ToList();

    public PageList<MovieResponse> DomainPageListToResponsePageList(PageList<Movie> moviePageList) =>
        new()
        {
            CurrentPage = moviePageList.CurrentPage,
            PageSize = moviePageList.PageSize,
            Result = DomainListToResponseList(moviePageList.Result),
            TotalCount = moviePageList.TotalCount,
            TotalPages = moviePageList.TotalPages
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
