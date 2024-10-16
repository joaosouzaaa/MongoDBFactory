using MongoDBFactory.API.DataTransferObjects.Movies;
using MongoDBFactory.API.Settings.PaginationSettings;

namespace MongoDBFactory.API.Interfaces.Services;

public interface IMovieService
{
    Task AddAsync(CreateMovieRequest createMovieRequest, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<MovieResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<PageList<MovieResponse>> GetAllPaginatedAsync(PageParameters parameters, CancellationToken cancellationToken);
    Task<MovieResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateMovieRequest updateMovieRequest, CancellationToken cancellationToken);
}
