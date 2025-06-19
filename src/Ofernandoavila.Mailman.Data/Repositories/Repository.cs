using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Business.Models;
using Ofernandoavila.Mailman.Data.Context;
using Ofernandoavila.Mailman.Data.Extensions;

namespace Ofernandoavila.Mailman.Data.Repositories;

public abstract class Repository<TEntity>(AppDbContext db) : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly AppDbContext Db = db;
    protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();
    public virtual async Task<int> Add(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return 1;
    }

    public virtual async Task<int> Delete(Guid id)
    {
        var e = await DbSet.FindAsync(id);
        DbSet.Remove(e);
        return 1;
    }

    public void Dispose()
    {
        Db?.Dispose();
        GC.SuppressFinalize(this);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool desc = false)
    {
        var list = await DbSet.Where(predicate)
                                .OrderByCustom(orderBy, desc)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return list;
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<int> GetTotal(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate)
                            .CountAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public virtual async Task<int> Update(TEntity entity)
    {
        var e = await DbSet.FindAsync(entity.Id);
        Db.Entry(e).CurrentValues.SetValues(entity);
        return 1;
    }
}