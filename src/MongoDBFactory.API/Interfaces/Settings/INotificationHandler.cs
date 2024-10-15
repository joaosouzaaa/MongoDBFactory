using MongoDBFactory.API.Settings.NotificationSettings;

namespace MongoDBFactory.API.Interfaces.Settings;

public interface INotificationHandler
{
    void AddNotification(string key, string message);
    List<Notification> GetNotifications();
    bool HasNotifications();
}
