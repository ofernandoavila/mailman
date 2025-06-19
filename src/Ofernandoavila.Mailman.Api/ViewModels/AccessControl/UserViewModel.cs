using Ofernandoavila.Mailman.Api.ViewModels.Parameter;

namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl;

public class UserViewModel : EntityViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool FirstAccesss { get; set; }
    public DateTime CreatedAt { get; set; }
    public RoleViewModel Role { get; set; }
}