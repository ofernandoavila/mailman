namespace Ofernandoavila.Mailman.Business.Models.AccessControl;

public class Session : Entity
{
    public string UserAgent { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime ExpirationTime { get; private set; }
    public string Token { get; private set; }
    public string RefreshToken { get; private set; }
    public Guid UserId { get; set; }

    public User User;

    public Session()
    {

    }

    public Session(Guid id, string userAgent, DateTime expirationTime, string token, string refreshToken, Guid userId)
    {
        Id = id;
        UserAgent = userAgent;
        ExpirationTime = expirationTime;
        Token = token;
        RefreshToken = refreshToken;
        UserId = userId;
    }
}