using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Settings.PaginationSettings;

namespace MongoDBFactory.API.Interfaces.Repositories;

public interface IMovieRepository
{
    Task AddAsync(Movie movie, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken);
    Task<PageList<Movie>> GetAllPaginatedAsync(PageParameters pageParameters, CancellationToken cancellationToken);
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken);
}
