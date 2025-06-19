using FluentAssertions;
using Moq;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Models.Notification;
using Ofernandoavila.Mailman.Business.Models.Settings;
using Ofernandoavila.Mailman.Business.Tests._Fixture.Notification;

namespace Ofernandoavila.Mailman.Business.Tests.Notification
{
    [Collection(nameof(NotificatorCollection))]
    [Trait("Unit Test", "Notificator")]
    public class NotificatorTests
    {
        private readonly NotificatorTestsFixture _notificatorTestsFixture;
        private readonly Notificator _notificator;

        public NotificatorTests(NotificatorTestsFixture notificator)
        {
            _notificatorTestsFixture = notificator;
            _notificator = _notificatorTestsFixture.CreateService();
        }

        [Fact(DisplayName = "Notificator Handle Not Empty")]
        public void Notificator_HandleNotEmpty()
        {
            var notification = _notificatorTestsFixture.GenerateNotification();
            var notificatorMock = new Mock<INotificator>();

            notificatorMock.Object.Handle(notification);

            notificatorMock.Verify(r => r.Handle(notification), Times.Once);
        }

        [Fact(DisplayName = "Notificator Handle Empty")]
        public void Notificator_HandleEmpty()
        {
            var notificatorMock = new Mock<INotificator>();

            notificatorMock.Object.Handle(null);

            notificatorMock.Verify(r => r.Handle(null), Times.Once);
        }

        [Fact(DisplayName = "Notificator GetNotifications Not Empty")]
        public void Notificator_GetNotificationsNotEmpty()
        {
            var notification = _notificatorTestsFixture.GenerateNotification();
            var list = new List<Business.Models.Settings.Notification>() { notification };
            var notificatorMock = new Mock<INotificator>();

            notificatorMock.Setup(s => s.Handle(null))
                            .Verifiable();

            notificatorMock.Setup(s => s.GetNotifications())
                            .Returns(list);

            var result = notificatorMock.Object.GetNotifications();

            result.Should().HaveCount(1);

            notificatorMock.Verify(r => r.GetNotifications(), Times.Once);
        }

        [Fact(DisplayName = "Notificator GetNotifications Empty")]
        public void Notificator_GetNotificationsEmpty()
        {
            var list = new List<Business.Models.Settings.Notification>();
            var notificatorMock = new Mock<INotificator>();

            notificatorMock.Setup(s => s.Handle(null))
                            .Verifiable();

            notificatorMock.Setup(s => s.GetNotifications())
                            .Returns(list);

            var result = notificatorMock.Object.GetNotifications();

            result.Should().HaveCount(0);

            notificatorMock.Verify(r => r.GetNotifications(), Times.Once);
        }

        [Fact(DisplayName = "Notificator HasNotifications Must Return True")]
        public void Notificator_HasNotifications_MustReturnTrue()
        {
            var notificatorMock = new Mock<INotificator>();

            notificatorMock.Setup(s => s.HasNotification())
                            .Returns(true);

            var result = notificatorMock.Object.HasNotification();

            result.Should().BeTrue();

            notificatorMock.Verify(r => r.HasNotification(), Times.Once);
        }

        [Fact(DisplayName = "Notificator HasNotifications Must Return False")]
        public void Notificator_HasNotifications_MustReturnFalse()
        {
            var notificatorMock = new Mock<INotificator>();

            notificatorMock.Setup(s => s.HasNotification())
                            .Returns(false);

            var result = notificatorMock.Object.HasNotification();

            result.Should().BeFalse();

            notificatorMock.Verify(r => r.HasNotification(), Times.Once);
        }
    }
}
