using FluentAssertions;
using System.Net;
using Ofernandoavila.Mailman.API.Tests.AccessControl.Fixture;

namespace Ofernandoavila.Mailman.API.Tests.AccessControl
{
    [Collection(nameof(UserCollection))]
    [Trait("API Integration", "User")]
    [TestCaseOrderer("Ofernandoavila.Mailman.API.Tests.PriorityOrderer", "Ofernandoavila.Mailman.API.Tests")]
    public class UserControllerIntegrationTests(UserFixture fixture)
    {
        private readonly UserFixture _fixture = fixture;

        [Fact(DisplayName = "UserController Add must be successful")]
        public async Task UserController_Add_MustBeSuccessful()
        {
            _fixture.RecreateDatabase();

            var user = _fixture.AddUser();
            var response = await _fixture.PostUserResponse(user);
            var objResponse = await _fixture.DesealizeUserWriteResponseModel(response);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "UserController Add must fail")]
        public async Task UserController_Add_MustFail()
        {
            var user = _fixture.AddUser();
            user.Email = "google.com";

            var response = await _fixture.PostUserResponse(user);
            var objResponse = await _fixture.DesealizeUserWriteResponseModel(response);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            objResponse.Errors.Should().NotBeNullOrEmpty();
            objResponse.Success.Should().BeFalse();
            objResponse.Data.Should().BeNullOrEmpty();
        }
    }
}
