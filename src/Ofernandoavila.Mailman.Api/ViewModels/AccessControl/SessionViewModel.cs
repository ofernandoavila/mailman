namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl;

public class SessionViewModel
{
    public string UserAgent { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime ExpirationDateTime { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public UserViewModel UserViewModel { get; set; }
}