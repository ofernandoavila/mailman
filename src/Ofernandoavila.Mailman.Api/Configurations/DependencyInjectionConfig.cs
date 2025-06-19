using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Ofernandoavila.Mailman.Api.Configurations.Swagger;
using Ofernandoavila.Mailman.Api.Extensions;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.Parameter;
using Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Services.Parameter;
using Ofernandoavila.Mailman.Business.Interfaces.User;
using Ofernandoavila.Mailman.Business.Models.Notification;
using Ofernandoavila.Mailman.Business.Models.Services.AccessControl;
using Ofernandoavila.Mailman.Business.Models.Services.Parameter;
using Ofernandoavila.Mailman.Data.Context;
using Ofernandoavila.Mailman.Data.Repositories;
using Ofernandoavila.Mailman.Data.Repositories.AccessControl;
using Ofernandoavila.Mailman.Data.Repositories.Parameter;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ofernandoavila.Mailman.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<AppDbContext>();
        services.AddScoped<INotificator, Notificator>();
        services.AddScoped<IUser, AppUser>();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}