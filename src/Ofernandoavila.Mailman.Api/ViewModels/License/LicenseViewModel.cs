using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.Business.Models.AccessControl;

namespace Ofernandoavila.Mailman.Api.ViewModels.License;

public class LicenseViewModel : EntityViewModel
{
    public string ApplicationName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ValidUntil { get; set; }
    public string Hosts { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}