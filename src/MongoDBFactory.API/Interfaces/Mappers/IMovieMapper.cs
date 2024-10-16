using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Settings.PaginationSettings;

namespace MongoDBFactory.API.Interfaces.Mappers;

public interface IMovieMapper
{
    Movie CreateRequestToDomain(CreateMovieRequest createMovieRequest);
    List<MovieResponse> DomainListToResponseList(List<Movie> movieList);
    PageList<MovieResponse> DomainPageListToResponsePageList(PageList<Movie> moviePageList);
    MovieResponse DomainToResponse(Movie movie);
    Movie UpdateRequestToDomain(UpdateMovieRequest updateMovieRequest);
}
