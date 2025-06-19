using System.Net;

namespace Ofernandoavila.Mailman.Api.Extensions;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        ex.Data.Add("Url", $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}");
        ex.Data.Add("Request body", await GetRequestBody(context));
        ex.Data.Add("QueryString", context.Request.QueryString.ToString());
        ex.Data.Add("Exception Message", ex.Message);
    }

    public static async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();
        context.Request.Body.Position = 0;

        using var reader = new StreamReader(context.Request.Body);
        return await reader.ReadToEndAsync();
    }
}
