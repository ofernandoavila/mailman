using System;

namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl;

public class UserLoginResponseViewModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenViewModel UserToken { get; set; }
}