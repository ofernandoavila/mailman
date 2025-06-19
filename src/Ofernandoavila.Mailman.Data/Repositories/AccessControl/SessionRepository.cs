using Microsoft.EntityFrameworkCore;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.AccessControl;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using Ofernandoavila.Mailman.Data.Context;

namespace Ofernandoavila.Mailman.Data.Repositories.AccessControl;

public class SessionRepository(AppDbContext context) : Repository<Session>(context), ISessionRepository
{
    public override async Task<Session> GetById(Guid id)
    {
        return await Db.Session
                        .AsNoTracking()
                        .FirstOrDefaultAsync( s => s.Id == id);
    }

    public async Task<Session> GetByRefreshToken(string refreshToken, int expirationRefreshTokenMinutes)
    {
        return await Db.Session
                        .AsNoTracking()
                        .FirstOrDefaultAsync( s => s.RefreshToken == refreshToken &&
                                                    s.ExpirationTime >= DateTime.UtcNow.AddMinutes(-expirationRefreshTokenMinutes));
    }
}