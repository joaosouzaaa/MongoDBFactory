using MongoDBFactory.API.Data.BaseMappings;
using System.Reflection;

namespace MongoDBFactory.API.Factories;

public static class MappingFactory
{
    public static void ConfigureMongoDbMappings()
    {
        var mappingTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(IsMappingClass)
            .ToList();

        foreach (var mappingType in mappingTypes)
        {
            dynamic mappingInstance = Activator.CreateInstance(mappingType)!;

            mappingInstance.Configure();
        }
    }

    private static bool IsMappingClass(Type type) =>
        type.IsClass &&
        type.IsSealed &&
        !type.IsAbstract &&
        type.BaseType is not null &&
        type.BaseType.IsGenericType &&
        type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityMapping<>);
}
