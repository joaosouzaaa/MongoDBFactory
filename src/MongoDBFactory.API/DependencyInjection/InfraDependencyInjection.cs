using MongoDB.Driver;
using MongoDBFactory.API.Constants;
using MongoDBFactory.API.Factories;
using MongoDBFactory.API.Options;

namespace MongoDBFactory.API.DependencyInjection;

internal static class InfraDependencyInjection
{
    internal static void AddInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        MappingFactory.ConfigureMongoDbMappings();

        var mongoDb = configuration.GetSection(OptionsConstants.MongoDBSection).Get<MongoDBOptions>()!;

        services.AddScoped<IMongoClient, MongoClient>(m => new MongoClient(mongoDb.ConnectionString));
        services.AddScoped(m => m.GetRequiredService<IMongoClient>().GetDatabase(mongoDb.DatabaseName));

        services.AddSettingsDependencyInjection();
        services.AddRepositoriesDependencyInjection();
    }
}
