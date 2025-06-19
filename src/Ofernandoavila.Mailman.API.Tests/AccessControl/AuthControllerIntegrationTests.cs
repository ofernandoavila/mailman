using FluentAssertions;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.API.Tests.AccessControl.Fixture;
using System.Net;

namespace Ofernandoavila.Mailman.API.Tests.AccessControl
{
    [Collection(nameof(AuthCollection))]
    [Trait("API Integration", "Auth")]
    [TestCaseOrderer("Ofernandoavila.Mailman.API.Tests.PriorityOrderer", "Ofernandoavila.Mailman.API.Tests")]
    public class AuthControllerIntegrationTests(AuthFixture fixture)
    {
        private readonly AuthFixture _fixture = fixture;

        [Theory(DisplayName = "AuthController Login must be successful")]
        [InlineData("fernandoavilajunior@gmail.com", "@Aa12345")]
        public async Task AuthController_Login_MustBeSuccessful(string email, string password)
        {
            var userLogin = new UserLoginViewModel() { Email = email, Password = password };
            var response = await _fixture.PostLoginResponse(userLogin);
            var userResponse = await _fixture.DeserializeUserLoginResponseModel(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            userResponse.Success.Should().BeTrue();
            userResponse.Errors.Should().BeNullOrEmpty();
            userResponse.Data.Should().NotBeNull();
            userResponse.Data.UserToken.Email.Should().Be(email);
        }

        [Theory(DisplayName = "AuthController Login must fail")]
        [InlineData("fernandoavilajunior@gmail.com", "@Aa1234567")]
        [InlineData("fernandoavilajunior@outlook.com", "@Aa12345")]
        public async Task AuthController_Login_MustFail(string email, string password)
        {
            var userLogin = new UserLoginViewModel() { Email = email, Password = password };
            var response = await _fixture.PostLoginResponse(userLogin);
            var userResponse = await _fixture.DeserializeUserLoginResponseModel(response);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            userResponse.Success.Should().BeFalse();
            userResponse.Errors.Should().NotBeNullOrEmpty();
            userResponse.Data.Should().BeNull();
        }

        [Theory(DisplayName = "AuthController Login invalid model state")]
        [InlineData("fernandoavilajunior.gmail.com.br", "@Aa123456")]
        [InlineData("fernandoavilajunior@gmail.com", "")]
        [InlineData("", "")]
        public async Task AuthController_Login_InvalidModelState(string email, string password)
        {
            var userLogin = new UserLoginViewModel() { Email = email, Password = password };
            var response = await _fixture.PostLoginResponse(userLogin);
            var userResponse = await _fixture.DeserializeUserLoginResponseModel(response);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            userResponse.Success.Should().BeFalse();
            userResponse.Errors.Should().NotBeNullOrEmpty();
            userResponse.Data.Should().BeNull();
        }
    }
}
