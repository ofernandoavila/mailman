using System.ComponentModel.DataAnnotations;

namespace Ofernandoavila.Mailman.Api.ViewModels.License;

public class LicenseInsertViewModel : EntityViewModel
{
    [Required(ErrorMessage = "The field {0} is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "The field {0} must be between {2} and {1} characters.")]
    public string ApplicationName { get; set; }
    [Required(ErrorMessage = "The field {0} is required.")]
    [StringLength(300, MinimumLength = 8, ErrorMessage = "The field {0} must be between {2} and {1} characters.")]
    public string Hosts { get; set; }
    public DateTime ValidUntil { get; set; }
}