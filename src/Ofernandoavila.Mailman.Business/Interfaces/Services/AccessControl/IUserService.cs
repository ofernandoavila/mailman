using System.Linq.Expressions;

namespace Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;

public interface IUserService : IDisposable
{
    Task<int> GetTotal(Expression<Func<Models.AccessControl.User, bool>> predicate);
    Task<IEnumerable<Models.AccessControl.User>> GetAll(int pageNumber, int pageSize, Expression<Func<Models.AccessControl.User, bool>> predicate, Expression<Func<Models.AccessControl.User, object>> OrderBy, bool desc);
    Task<Models.AccessControl.User> GetById(Guid id);
    Task<Models.AccessControl.User> GetByEmailAndPassword(string email, string password);
    Task<Models.AccessControl.User> CheckUserByEmail(string email);
    Task<bool> Add(Models.AccessControl.User usuario);
    Task<bool> Update(Models.AccessControl.User usuario);
    Task<bool> UpdatePassword(Models.AccessControl.User usuario, string newPassword);
    Task<bool> Delete(Guid id);
    Task<bool> UpdateStatus(Guid id);
    Task Complete();
}