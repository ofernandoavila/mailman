using System;

namespace Ofernandoavila.Mailman.Business.Models.Settings;

public class AppSettings
{
    public int TokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationMinutes { get; set; }
    public string Secret { get; set; }
    public string Emitter { get; set; }
    public string[] ValidIn { get; set; }

    public AppSettings()
    {

    }

    public AppSettings(int tokenExpirationMinutes, int refreshTokenExpirationMinutes, string secret, string emitter, string[] validIn)
    {
        TokenExpirationMinutes = tokenExpirationMinutes;
        RefreshTokenExpirationMinutes = refreshTokenExpirationMinutes;
        Secret = secret;
        Emitter = emitter;
        ValidIn = validIn;
    }
}