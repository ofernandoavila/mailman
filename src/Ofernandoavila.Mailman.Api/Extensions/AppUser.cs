using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Ofernandoavila.Mailman.Business.Interfaces.User;

namespace Ofernandoavila.Mailman.Api.Extensions;

[ExcludeFromCodeCoverage]
public class AppUser(IHttpContextAccessor accessor) : IUser
{
private readonly IHttpContextAccessor _accessor = accessor;
    public string Name => _accessor.HttpContext.User.Identity.Name;

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext.User.Claims;
    }

    public bool GetFirstAccess()
    {
        return Convert.ToBoolean(_accessor.HttpContext.User.FindFirst("FirstAccess").Value);
    }

    public string GetUserAgent()
    {
        var containsKey = _accessor.HttpContext.Request.Headers.ContainsKey("User-Agent");
        return containsKey ? _accessor.HttpContext.Request.Headers.UserAgent : _accessor.HttpContext.Request.Headers.Host;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
    }

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
    }

    public string GetUserRole()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserRole() : string.Empty;
    }

    public async Task<string> GetUserToken()
    {
        return await _accessor.HttpContext.GetTokenAsync("access_token");
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool IsInRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }
}

[ExcludeFromCodeCoverage]
public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value;
    }

    public static string GetUserRole(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Role);
        return claim?.Value;
    }
}