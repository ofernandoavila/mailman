using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Ofernandoavila.Mailman.Api;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.API.Tests.AccessControl.ResponseModel;
using Ofernandoavila.Mailman.Data.Context;

namespace Ofernandoavila.Mailman.API.Tests._Config
{
    public abstract class IntegrationTest : IDisposable
    {
        protected HttpClient _client;
        protected UserLoginResponseModel _user;
        protected WebApplicationFactory<Startup> _factory;
        protected IntegrationTest(string baseAddress = "localhost", int maxRedirections = 7)
        {
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });

            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri($"https://{baseAddress}"),
                HandleCookies = true,
                MaxAutomaticRedirections = maxRedirections
            };

            _client = _factory.CreateClient(clientOptions);

        }

        public virtual void RecreateDatabase()
        {
            using var scope = _factory.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetService<AppDbContext>();

            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        public async Task Authentication(string email = "", string password = "", bool authenticate = true)
        {
            if (authenticate)
                await Auth(email, password);
            else
                _client.DefaultRequestHeaders.Clear();
        }

        private async Task Auth(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) email = "fernandoavilajunior@gmail.com";
            if (string.IsNullOrEmpty(password)) password = "@Aa12345";

            var userLogin = new UserLoginViewModel() { Email = email, Password = password };

            var response = await _client.PostAsJsonAsync("api/v1/login", userLogin);
            response.EnsureSuccessStatusCode();
            var usuarioResponse = await response.Content.ReadAsStringAsync();
            _user = JsonConvert.DeserializeObject<UserLoginResponseModel>(usuarioResponse);

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _user.Data.AccessToken);
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
