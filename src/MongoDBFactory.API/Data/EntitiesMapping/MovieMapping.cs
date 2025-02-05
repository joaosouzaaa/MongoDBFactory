using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDBFactory.API.Data.BaseMappings;
using MongoDBFactory.API.Entities;

namespace MongoDBFactory.API.Data.EntitiesMapping;

public sealed class MovieMapping : BaseEntityMapping<Movie>
{
    protected override void Map(BsonClassMap<Movie> classMap)
    {
        classMap.AutoMap();

        classMap.MapIdField(m => m.Id)
            .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

        classMap.MapProperty(m => m.Title)
            .SetIsRequired(true)
            .SetElementName("title");

        classMap.MapProperty(m => m.Genre)
            .SetIsRequired(true)
            .SetElementName("genre");

        classMap.MapProperty(m => m.ReleaseYear)
            .SetIsRequired(true)
            .SetElementName("release_year");

        classMap.MapProperty(m => m.Director)
            .SetIsRequired(true)
            .SetElementName("director");
    }
}
