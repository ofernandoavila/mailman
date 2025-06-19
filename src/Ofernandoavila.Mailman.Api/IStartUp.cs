namespace Ofernandoavila.Mailman.Api;

public interface IStartUp
{
    IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services);
    public void Configure(WebApplication app, IWebHostEnvironment env);
}
