using FluentAssertions;
using Ofernandoavila.Mailman.Business.Models.Settings;

namespace Ofernandoavila.Mailman.Business.Tests.Models.Settings
{
    [Trait("Unit Test", "Models Settings AppSettings")]
    public class AppSettingsTests
    {
        [Fact(DisplayName = "Verify AppSettingsDefault Type")]
        public void Settings_AppSettings_VerifyType()
        {
            var result = new AppSettings();

            result.Should().BeOfType<AppSettings>();
        }

        [Fact(DisplayName = "Verify AppSettingsOverride Type")]
        public void Settings_AppSettings_VerifyOverrideType()
        {
            var result = new AppSettings(10, 5, "Mock Secret", "Mock Emitter", ["Mock ValidIn"]);

            result.Should().BeOfType<AppSettings>();
        }
    }
}
