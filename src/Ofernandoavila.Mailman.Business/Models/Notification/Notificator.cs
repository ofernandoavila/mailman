using Ofernandoavila.Mailman.Business.Interfaces.Notification;

namespace Ofernandoavila.Mailman.Business.Models.Notification;

public class Notificator : INotificator
{
    private readonly List<Models.Settings.Notification> _notifications;
    public Notificator()
    {
        _notifications = [];
    }
    public List<Models.Settings.Notification> GetNotifications()
    {
        return _notifications;
    }

    public void Handle(Models.Settings.Notification notification)
    {
        _notifications.Add(notification);
    }

    public bool HasNotification()
    {
        return _notifications.Count != 0;
    }
}