using MongoDBFactory.API.Data.Repositories;
using MongoDBFactory.API.Interfaces.Repositories;

namespace MongoDBFactory.API.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IMovieRepository, MovieRepository>();
}
