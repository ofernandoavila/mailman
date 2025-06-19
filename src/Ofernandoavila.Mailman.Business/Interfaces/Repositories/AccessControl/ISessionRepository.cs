using Ofernandoavila.Mailman.Business.Models.AccessControl;

namespace Ofernandoavila.Mailman.Business.Interfaces.Repositories.AccessControl;

public interface ISessionRepository : IRepository<Session>
{
    Task<Session> GetByRefreshToken(string refreshToken, int expirationRefreshTokenMinutes);
}