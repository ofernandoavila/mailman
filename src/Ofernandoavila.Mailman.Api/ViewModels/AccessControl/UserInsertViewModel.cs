using Ofernandoavila.Mailman.Api.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl
{
    public class UserInsertViewModel : EntityViewModel
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The field {0} must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z0-9-._]+@[a-zA-Z0-9_-]+?\.[a-zA-Z.-]{2,30}$",
            ErrorMessage = "The field {0} must be a valid e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The field {0} must be between {2} and {1} characters.")]
        public string Name { get; set; }

        [NotEmpty(ErrorMessage = "The field {0} is required.")]
        public Guid RoleId { get; set; }
    }
}
