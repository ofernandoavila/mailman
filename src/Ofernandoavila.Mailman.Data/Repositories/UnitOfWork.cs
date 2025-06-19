using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories.Parameter;
using Ofernandoavila.Mailman.Data.Context;
using Ofernandoavila.Mailman.Data.Repositories.AccessControl;
using Ofernandoavila.Mailman.Data.Repositories.Parameter;

namespace Ofernandoavila.Mailman.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext, IMemoryCache memoryCache) : IUnitOfWork
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IMemoryCache _memoryCache = memoryCache;

    private IUserRepository _userRepository;
    public IUserRepository UserRepository { get => _userRepository ??= new UserRepository(_dbContext); }
    private ISessionRepository _sessionRepository;
    public ISessionRepository SessionRepository { get => _sessionRepository ??= new SessionRepository(_dbContext);  }
    private IRoleRepository _roleRepository;
    public IRoleRepository RoleRepository { get => _roleRepository ??= new RoleRepository(_dbContext); }

    public async Task<int> Complete()
    {
        var complete = 0;

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            
            complete = await _dbContext.SaveChangesAsync();

            transaction.Commit();
        });

        return complete;
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
        GC.SuppressFinalize(this);
    }
}