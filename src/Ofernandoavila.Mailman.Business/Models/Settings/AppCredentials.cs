using System;

namespace Ofernandoavila.Mailman.Business.Models.Settings;

public class AppCredentials
{
    public string AppUser { get; set; }
    public string AppPassword { get; set; }

    public AppCredentials()
    {

    }

    public AppCredentials(string appUser, string appPassword)
    {
        AppUser = appUser;
        AppPassword = appPassword;
    }
}