using Asp.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Ofernandoavila.Mailman.Api.Extensions;

namespace Ofernandoavila.Mailman.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static IServiceCollection ApiBehaviourConfig(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>( options =>
                                                {
                                                    options.SuppressModelStateInvalidFilter = true;
                                                });

        services.AddControllers()
                .AddNewtonsoftJson( options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

        return services;
    }

    public static IServiceCollection ApiVersioningConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection ApiCorsConfig(this IServiceCollection services)
    {
        services.AddCors( options =>
        {
            options.AddPolicy("Default", builder => builder.AllowAnyOrigin()
                                                            .AllowAnyMethod()
                                                            .AllowAnyHeader());

            options.AddPolicy("Production", builder => builder.AllowAnyOrigin()
                                                            .AllowAnyMethod()
                                                            .AllowAnyHeader());
        });

        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Use(async (context, next) => {
            context.Request.EnableBuffering();
            await next();
        });

        if(env.IsProduction())
        {
            app.UseCors("Production");
            app.UseHsts();
        } else {
            app.UseCors("Default");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<SecurityMiddleware>(env);
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    public static IApplicationBuilder UseEndPointsConfiguration(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = "/monitor";
                options.ApiPath = "/health-ui-api";
                options.WebhookPath = "/da-webhooks";
                options.ResourcesPath = "/my-resources";
                options.UseRelativeApiPath = true;
                options.UseRelativeResourcesPath = true;
                options.UseRelativeWebhookPath = true;
                options.AddCustomStylesheet("wwwroot/hc-ui.css");
            });
        });

        return app;
    }
}