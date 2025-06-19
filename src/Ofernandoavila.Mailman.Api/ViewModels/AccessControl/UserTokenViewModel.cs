using System;

namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl;

public class UserTokenViewModel
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public bool Active { get; set; }
    public bool FirstAccess { get; set; }
    public string RefreshToken { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}