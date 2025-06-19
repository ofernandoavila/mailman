using FluentAssertions;
using Ofernandoavila.Mailman.Business.Models.Settings;

namespace Ofernandoavila.Mailman.Business.Tests.Models.Settings
{
    [Trait("Unit Test", "Models Settings AppCredentials")]
    public class AppCredentialsTests
    {
        [Fact(DisplayName = "Verify AppCredentialsDefault Type")]
        public void Settings_AppCredentials_VerifyType()
        {
            var result = new AppCredentials();

            result.Should().BeOfType<AppCredentials>();
        }

        [Fact(DisplayName = "Verify AppCredentialsOverride Type")]
        public void Settings_AppCredentials_VerifyOverrideType()
        {
            var result = new AppCredentials("Admin", "123");

            result.Should().BeOfType<AppCredentials>();
        }
    }
}
