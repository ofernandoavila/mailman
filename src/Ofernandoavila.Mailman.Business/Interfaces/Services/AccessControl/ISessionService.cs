using Ofernandoavila.Mailman.Business.Models.AccessControl;

namespace Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;

public interface ISessionService : IDisposable
{
    Task<Session> GetByRefreshToken(string refreshToken, int expirationRefreshTokenMinutes);
    Task<bool> Add(Session session);
    Task<bool> Update(Session session);
    Task Complete();
}