using MongoDB.Driver;

namespace MongoDBFactory.API.Settings.PaginationSettings;

public static class PaginationHandler
{
    public static async Task<PageList<TEntity>> PaginateAsync<TEntity>(
        this IFindFluent<TEntity, TEntity> query,
        PageParameters pageParameters,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var count = await query.CountDocumentsAsync(cancellationToken);

        var entityPaginatedList = await query
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Limit(pageParameters.PageSize)
            .ToListAsync(cancellationToken);

        return new PageList<TEntity>(entityPaginatedList, count, pageParameters);
    }
}
