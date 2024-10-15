using MongoDB.Driver;
using MongoDBFactory.API.Constants;
using MongoDBFactory.API.Entities;
using MongoDBFactory.API.Interfaces.Repositories;
using MongoDBFactory.API.Settings.PaginationSettings;

namespace MongoDBFactory.API.Data.Repositories;

public sealed class MovieRepository(IMongoDatabase dbContext) : IMovieRepository
{
    private readonly IMongoCollection<Movie> _collection = dbContext.GetCollection<Movie>(CollectionConstants.MovieCollection);

    public Task AddAsync(Movie movie, CancellationToken cancellationToken) =>
        _collection.InsertOneAsync(movie, cancellationToken: cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        _collection.DeleteOneAsync(m => m.Id == id, cancellationToken);

    public Task<List<Movie>> GetAllAsync(CancellationToken cancellationToken) =>
        _collection.Find(Builders<Movie>.Filter.Empty).ToListAsync(cancellationToken);

    public Task<PageList<Movie>> GetAllPaginatedAsync(PageParameters pageParameters, CancellationToken cancellationToken) =>
        _collection.Find(Builders<Movie>.Filter.Empty).PaginateAsync(pageParameters, cancellationToken);

    public Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        _collection.Find(m => m.Id == id).FirstOrDefaultAsync(cancellationToken)!;

    public Task UpdateAsync(Movie movie, CancellationToken cancellationToken) =>
        _collection.ReplaceOneAsync(m => m.Id == movie.Id, movie, cancellationToken: cancellationToken);
}
