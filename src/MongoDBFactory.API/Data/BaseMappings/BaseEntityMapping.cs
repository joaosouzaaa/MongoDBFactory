using MongoDB.Bson.Serialization;

namespace MongoDBFactory.API.Data.BaseMappings;

public abstract class BaseEntityMapping<TEntity>
    where TEntity : class
{
    public void Configure() =>
        BsonClassMap.RegisterClassMap<TEntity>(Map);

    protected abstract void Map(BsonClassMap<TEntity> classMap);
}
