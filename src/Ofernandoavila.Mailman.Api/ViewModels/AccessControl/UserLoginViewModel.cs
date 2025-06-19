using System.ComponentModel.DataAnnotations;

namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl;

public class UserLoginViewModel
{
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "This field '{0}' is requried.")]
    public string Email { get; set; }
    [Display(Name = "Password")]
    [Required(ErrorMessage = "This field '{0}' is requried.")]
    public string Password { get; set; }
}