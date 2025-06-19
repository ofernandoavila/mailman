using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Business.Interfaces.Services.Parameter;
using Ofernandoavila.Mailman.Business.Models.Parameter;
using System.Linq.Expressions;

namespace Ofernandoavila.Mailman.Business.Models.Services.Parameter
{
    public class RoleService(IUnitOfWork unitOfWork,
                            INotificator notificator) : BaseService(notificator), IRoleService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> GetTotal(Expression<Func<Role, bool>> predicate)
        {
            return await _unitOfWork.RoleRepository.GetTotal(predicate);
        }

        public async Task<IEnumerable<Role>> GetAll(int pageNumber, int pageSize, Expression<Func<Role, bool>> predicate, Expression<Func<Role, object>> orderBy, bool desc)
        {
            return await _unitOfWork.RoleRepository.GetAll(pageNumber, pageSize, predicate, orderBy, desc);
        }

        public async Task<Role> GetById(Guid id)
        {
            return await _unitOfWork.RoleRepository.GetById(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Complete()
        {
            await _unitOfWork.Complete();
        }
    }
}
