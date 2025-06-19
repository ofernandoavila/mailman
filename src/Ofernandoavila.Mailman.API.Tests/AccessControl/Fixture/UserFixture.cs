using Newtonsoft.Json;
using System.Net.Http.Json;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.API.Tests._Config;
using Ofernandoavila.Mailman.API.Tests.AccessControl.ResponseModel;

namespace Ofernandoavila.Mailman.API.Tests.AccessControl.Fixture
{
    [CollectionDefinition(nameof(UserCollection))]
    public class UserCollection : ICollectionFixture<UserFixture>
    { }

    public class UserFixture : IntegrationTest
    {
        private readonly string ControllerRoute = "api/v1/user";

        public UserFixture()
        {
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<HttpResponseMessage> GetByIdResponse(Guid id, bool authenticate = true, string email = "", string password = "")
        {
            await Authentication(email, password, authenticate);

            return await _client.GetAsync($"{ControllerRoute}/{id}");
        }

        public async Task<HttpResponseMessage> PostUserResponse(UserInsertViewModel user, bool authenticate = true, string email = "", string password = "")
        {
            await Authentication(email, password, authenticate);

            return await _client.PostAsJsonAsync($"{ControllerRoute}", user);
        }

        public async Task<UserInsertResponseModel> DesealizeUserWriteResponseModel(HttpResponseMessage response)
        {
            var objResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserInsertResponseModel>(objResponse);
        }

        public async Task<UserResponseModel> DeserializeGetByIdResponseModel(HttpResponseMessage response)
        {
            var objResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserResponseModel>(objResponse);
        }

        public UserInsertViewModel AddUser()
        {
            return new UserInsertViewModel
            {
                Id = Guid.Parse(Guid.NewGuid().ToString()),
                Email = "fernandoavilajunior@outlook.com.br",
                Name = "Fernando Avila",
                RoleId = Guid.Parse("775611a5-7f0b-46b9-8a2c-1f2526d865e5")
            };
        }
    }
}
