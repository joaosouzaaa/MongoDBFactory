namespace MongoDBFactory.API.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();
        services.AddOptionsDependencyInjection(configuration);
        services.AddInfraDependencyInjection(configuration);
        services.AddSettingsDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
