using FluentValidation;
using MongoDBFactory.API.Filters;
using MongoDBFactory.API.Interfaces.Settings;
using MongoDBFactory.API.Settings.NotificationSettings;
using System.Reflection;

namespace MongoDBFactory.API.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
        services.AddScoped<NotificationFilter>();

        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
    }
}
