using Microsoft.EntityFrameworkCore;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.Parameter;
using Ofernandoavila.Mailman.Business.Models.Parameter;
using Ofernandoavila.Mailman.Data.Context;

namespace Ofernandoavila.Mailman.Data.Repositories.Parameter;

public class RoleRepository(AppDbContext context) : Repository<Role>(context), IRoleRepository
{
    public override Task<Role> GetById(Guid id)
    {
        return Db.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync( r => r.Id == id);
    }
}