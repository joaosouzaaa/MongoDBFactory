using MongoDBFactory.API.Interfaces.Mappers;
using MongoDBFactory.API.Mappers;

namespace MongoDBFactory.API.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDirectorMapper, DirectorMapper>();
        services.AddScoped<IMovieMapper, MovieMapper>();
    }
}
