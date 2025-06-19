using Ofernandoavila.Mailman.Api.Configurations;
using Ofernandoavila.Mailman.Api.Configurations.Swagger;

namespace Ofernandoavila.Mailman.Api;

public class Startup(IConfiguration configuration) : IStartUp
{
    public IConfiguration Configuration => configuration;

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.MigrateDatabase();
        app.UseApiConfiguration(env);
        app.UseSwaggerConfig();
        app.UseLoggerConfiguration();
        app.UseEndPointsConfiguration();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabase(Configuration);
        services.AddAppCredentialsSettingsConfiguration(Configuration);
        services.AddAppSettingsConfiguration(Configuration);
        services.AddJWTConfiguration(Configuration);
        services.AddAutoMapper(typeof(AutomapperConfig));
        services.AddLoggerConfig(Configuration);
        services.ApiBehaviourConfig();
        services.ApiVersioningConfig();
        services.ApiCorsConfig();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfig();
        services.ResolveDependencies();
    }
}
