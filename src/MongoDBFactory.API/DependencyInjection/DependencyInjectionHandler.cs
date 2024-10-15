using MongoDB.Driver;
using MongoDBFactory.API.Constants;
using MongoDBFactory.API.Factories;
using MongoDBFactory.API.Options;

namespace MongoDBFactory.API.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();
        services.AddOptionsDependencyInjection(configuration);

        MappingFactory.ConfigureMongoDbMappings();

        var mongoDb = configuration.GetSection(OptionsConstants.MongoDBSection).Get<MongoDBOptions>()!;

        services.AddScoped<IMongoClient, MongoClient>(m => new MongoClient(mongoDb.ConnectionString));
        services.AddScoped(m => m.GetRequiredService<IMongoClient>().GetDatabase(mongoDb.DatabaseName));

        services.AddRepositoriesDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
