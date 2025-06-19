using System.Diagnostics.CodeAnalysis;

namespace Ofernandoavila.Mailman.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartUp>(this WebApplicationBuilder webAppBuilder) where TStartUp : IStartUp
    {
        if (Activator.CreateInstance(typeof(TStartUp), webAppBuilder.Configuration) is not IStartUp startUp)
            throw new ArgumentException("Startup.cs class is not valid!");

        startUp.ConfigureServices(webAppBuilder.Services);

        var app = webAppBuilder.Build();

        startUp.Configure(app, app.Environment);
        app.Run();

        return webAppBuilder;
    }
}
