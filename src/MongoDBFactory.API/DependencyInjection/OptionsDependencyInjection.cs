using MongoDBFactory.API.Constants;
using MongoDBFactory.API.Options;

namespace MongoDBFactory.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<MongoDBOptions>(configuration.GetSection(OptionsConstants.MongoDBSection));
}
