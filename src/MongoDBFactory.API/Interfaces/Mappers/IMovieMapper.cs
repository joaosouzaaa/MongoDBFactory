using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Entities;

namespace MongoDBFactory.API.Interfaces.Mappers;

public interface IMovieMapper
{
    Movie CreateRequestToDomain(CreateMovieRequest createMovieRequest);
    MovieResponse DomainToResponse(Movie movie);
    Movie UpdateRequestToDomain(UpdateMovieRequest updateMovieRequest);
}
