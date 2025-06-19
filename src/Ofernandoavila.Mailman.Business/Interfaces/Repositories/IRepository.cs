using System.Linq.Expressions;
using Ofernandoavila.Mailman.Business.Models;

namespace Ofernandoavila.Mailman.Business.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<int> GetTotal(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool desc = false);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

    Task<int> Add(TEntity entity);
    Task<int> Update(TEntity entity);
    Task<int> Delete(Guid id);
}