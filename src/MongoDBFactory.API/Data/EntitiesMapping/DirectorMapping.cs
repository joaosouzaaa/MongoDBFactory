using MongoDB.Bson.Serialization;
using MongoDBFactory.API.Data.BaseMappings;
using MongoDBFactory.API.Entities;

namespace MongoDBFactory.API.Data.EntitiesMapping;

public sealed class DirectorMapping : BaseEntityMapping<Director>
{
    protected override void Map(BsonClassMap<Director> classMap)
    {
        classMap.AutoMap();

        classMap.MapProperty(d => d.Name)
            .SetIsRequired(true)
            .SetElementName("name");

        classMap.MapProperty(d => d.BirthDate)
            .SetIsRequired(true)
            .SetElementName("birth_date");

        classMap.MapProperty(d => d.Nationality)
            .SetIsRequired(true)
            .SetElementName("nationality");

    }
}
