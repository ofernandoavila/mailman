using Ofernandoavila.Mailman.Business.Interfaces.Repositories.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.Parameter;

namespace Ofernandoavila.Mailman.Business.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ISessionRepository SessionRepository { get; }
    IRoleRepository RoleRepository { get; }

    Task<int> Complete();
}