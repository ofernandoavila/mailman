namespace Ofernandoavila.Mailman.Business.Interfaces.Repositories.AccessControl;

public interface IUserRepository : IRepository<Models.AccessControl.User>
{
    Task<Models.AccessControl.User> GetUserByEmail(string email);
    Task<Models.AccessControl.User> GetUserByEmailAndPassword(string email, string password);
}