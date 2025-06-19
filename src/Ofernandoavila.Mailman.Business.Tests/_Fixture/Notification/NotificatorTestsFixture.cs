using Moq.AutoMock;
using Ofernandoavila.Mailman.Business.Models.Notification;

namespace Ofernandoavila.Mailman.Business.Tests._Fixture.Notification
{
    [CollectionDefinition(nameof(NotificatorCollection))]
    public class NotificatorCollection : ICollectionFixture<NotificatorTestsFixture>
    { }

    public class NotificatorTestsFixture : IDisposable
    {
        public AutoMocker Mocker;
        public Notificator Notificator;

        public Notificator CreateService()
        {
            Mocker = new AutoMocker();
            Notificator = Mocker.CreateInstance<Notificator>();

            return Notificator;
        }

        public Business.Models.Settings.Notification GenerateNotification()
        {
            return new Business.Models.Settings.Notification("Mock Notification Error");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
