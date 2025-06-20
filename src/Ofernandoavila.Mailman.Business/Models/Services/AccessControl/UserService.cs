using System.Linq.Expressions;
using LinqKit;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using Ofernandoavila.Mailman.Business.Utils.Security;

namespace Ofernandoavila.Mailman.Business.Models.Services.AccessControl;

public class UserService(IUnitOfWork unitOfWork,
                            INotificator notificator) : BaseService(notificator), IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<bool> Add(User user)
    {
        if(!Validate(user)) return false;

        string password = string.IsNullOrEmpty(user.Password) ? "@Aa12345" : user.Password;

        user.EncryptPassword(password);
        user.SetFirstAccessFlag();

        await _unitOfWork.UserRepository.Add(user);

        return true;
    }

    public Task<User> CheckUserByEmail(string email)
    {
        return _unitOfWork.UserRepository.GetUserByEmail(email);
    }

    public async Task Complete()
    {
        await _unitOfWork.Complete();
    }

    public async Task<bool> Delete(Guid id)
    {
        if(_unitOfWork.UserRepository.GetById(id).Result is null)
            return Notificate("User not found.");

        if(_unitOfWork.SessionRepository.Search( s => s.UserId == id).Result.Any())
            return Notificate("Is not possible delete an user that made an operation on system");

        await _unitOfWork.UserRepository.Delete(id);
        return true;
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<User>> GetAll(int pageNumber, int pageSize, Expression<Func<User, bool>> predicate, Expression<Func<User, object>> orderBy, bool desc)
    {
        return await _unitOfWork.UserRepository.GetAll(pageNumber, pageSize, predicate, orderBy, desc);
    }

    public Task<User> GetByEmailAndPassword(string email, string password)
    {
        return _unitOfWork.UserRepository.GetUserByEmailAndPassword(email, SHA256Criptografy.Encrypt(password));
    }

    public async Task<User> GetById(Guid id)
    {
        return await _unitOfWork.UserRepository.GetById(id);
    }

    public async Task<int> GetTotal(Expression<Func<User, bool>> predicate)
    {
        return await _unitOfWork.UserRepository.GetTotal(predicate);
    }

    public async Task<bool> Update(User user)
    {
        if(!Validate(user, true)) return false;
    
        string hashedPassword = _unitOfWork.UserRepository.GetById(user.Id).Result.Password;

        if(user.Password is not null && user.Password != hashedPassword)
            user.EncryptPassword(user.Password);

        await _unitOfWork.UserRepository.Update(user);

        return true;
    }

    public async Task<bool> UpdatePassword(User user, string newPassword)
    {
        var entity = await _unitOfWork.UserRepository.GetById(user.Id);

        if(entity is null)
            return Notificate("User not found.");

        if(!user.IsValid(newPassword))
        {
            Notificate(user.ValidationResult);
            return false;
        }

        entity.EncryptPassword(newPassword);
        entity.SetFirstAccessFlag(false);

        await _unitOfWork.UserRepository.Update(entity);

        return true;
    }

    public async Task<bool> UpdateStatus(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetById(id);

        if(user is null)
            return Notificate("User not found.");

        if(user.Active)
            user.Desactivate();
        else
            user.Activate();

        await _unitOfWork.UserRepository.Update(user);

        return true;
    }

    private bool Validate(User user, bool isUpdate = false)
    {
        if(!IsValid(user)) return false;

        var expression = PredicateBuilder.New<User>( p => p.Email == user.Email);

        if(isUpdate)
            expression = expression.And( u => u.Id != user.Id);

        if(_unitOfWork.UserRepository.Search(expression).Result.Any())
            return Notificate("E-mail already register.");

        return true;
    }
}