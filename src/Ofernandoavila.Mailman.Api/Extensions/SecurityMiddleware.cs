using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace Ofernandoavila.Mailman.Api.Extensions;

[ExcludeFromCodeCoverage]
public class SecurityMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _swaggerUser;
    private readonly string _swaggerPassword;

    public SecurityMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        var appCredentials = new ConfigurationBuilder().AddJsonFile($"appsettings.{env.EnvironmentName}.json").Build();
        _swaggerUser = appCredentials.GetValue<string>("AppCredentials:AppUser");
        _swaggerPassword = appCredentials.GetValue<string>("AppCredentials:AppPassword");
    
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(!IsLocalRequest(context) &&
            context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.StartsWithSegments("/monitor"))
        {
            string authHeader = context.Request.Headers.Authorization;
            if(authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var username = decodedUsernamePassword.Split(":", 2)[0];
                var password = decodedUsernamePassword.Split(":", 2)[1];

                if(_swaggerUser.Equals(username, StringComparison.CurrentCultureIgnoreCase) && _swaggerPassword == password)
                {
                    await _next.Invoke(context);
                    return;
                }

                context.Response.Headers.WWWAuthenticate = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }
        else 
        {
            await _next.Invoke(context);
        }
    }

    private static bool IsLocalRequest(HttpContext context)
    {
        if(context.Request.Host.Value.StartsWith("localhost:"))
            return true;

        if(context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
            return true;

        if(context.Connection.RemoteIpAddress != null && context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
            return true;

        return IPAddress.IsLoopback(context.Connection.RemoteIpAddress);
    }
}