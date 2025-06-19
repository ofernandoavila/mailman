using Newtonsoft.Json;
using System.Net.Http.Json;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.API.Tests._Config;
using Ofernandoavila.Mailman.API.Tests.AccessControl.ResponseModel;

namespace Ofernandoavila.Mailman.API.Tests.AccessControl.Fixture
{
    [CollectionDefinition(nameof(AuthCollection))]
    public class AuthCollection : ICollectionFixture<AuthFixture>
    { }

    public class AuthFixture : IntegrationTest
    {
        private readonly string ControllerRoute = "api/v1/";

        public AuthFixture()
        {

        }

        public async Task<UserLoginResponseModel> Auth(string email, string password)
        {
            await Authentication(email, password);

            return _user;
        }

        public async Task<HttpResponseMessage> PostLoginResponse(UserLoginViewModel user)
        {
            return await _client.PostAsJsonAsync($"{ControllerRoute}login", user);
        }

        public async Task<UserLoginResponseModel> DeserializeUserLoginResponseModel(HttpResponseMessage response)
        {
            var objResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserLoginResponseModel>(objResponse);
        }

        public UserInsertViewModel SetLeadInsert()
        {
            return new UserInsertViewModel()
            {
                Id = Guid.Parse("f46fcc34-c057-40a7-bfd1-570562979318"),
                Email = "mailaplicacao@grupoccaa.com.br",
                Name = "Expedicao Digital Teste Insert",
                RoleId = Guid.Parse("775611a5-7f0b-46b9-8a2c-1f2526d865e5")
            };
        }
    }
}
