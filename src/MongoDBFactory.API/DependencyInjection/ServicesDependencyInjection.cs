using MongoDBFactory.API.Interfaces.Services;
using MongoDBFactory.API.Services;

namespace MongoDBFactory.API.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IMovieService, MovieService>();
}
