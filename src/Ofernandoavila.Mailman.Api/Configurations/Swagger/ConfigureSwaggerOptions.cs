using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ofernandoavila.Mailman.Api.Configurations.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach(var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            options.ResolveConflictingActions( apiDescriptions => apiDescriptions.First() );
        }
    }

    static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Ofernandoavila | Food Delivery Api",
            Version = "1.0.0",
            Description = "This API is part of Food Delivery App",
            Contact = new OpenApiContact() { Name = "Fernando Avila", Email = "fernandoavilajunior@gmail.com" }
        };

        if(description.IsDeprecated)
        {
            info.Description += " This version is deprecated!";
        }

        return info;
    }
}