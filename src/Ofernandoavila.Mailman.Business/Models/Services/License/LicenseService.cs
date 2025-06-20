using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Business.Interfaces.Services.License;
using System.Linq.Expressions;

namespace Ofernandoavila.Mailman.Business.Models.Services.License;

public class LicenseService(IUnitOfWork unitOfWork,
                            INotificator notificator) : BaseService(notificator), ILicenseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Add(Models.License.License license)
    {
        await _unitOfWork.LicenseRepository.Add(license);

        return true;
    }

    public async Task Complete()
    {
        await _unitOfWork.Complete();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _unitOfWork.LicenseRepository.Delete(id);

        return true;
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<Models.License.License>> GetAll(int pageNumber, int pageSize, Expression<Func<Models.License.License, bool>> predicate, Expression<Func<Models.License.License, object>> orderBy, bool desc)
    {
        return await _unitOfWork.LicenseRepository.GetAll(pageNumber, pageSize, predicate, orderBy, desc);
    }

    public async Task<Models.License.License> GetById(Guid id)
    {
        return await _unitOfWork.LicenseRepository.GetById(id);
    }

    public async Task<int> GetTotal(Expression<Func<Models.License.License, bool>> predicate)
    {
        return await _unitOfWork.LicenseRepository.GetTotal(predicate);
    }

    public async Task<bool> Update(Models.License.License license)
    {
        await _unitOfWork.LicenseRepository.Update(license);

        return true;
    }

    public async Task<bool> UpdateStatus(Guid id)
    {
        var license = await _unitOfWork.LicenseRepository.GetById(id);

        if (license is null)
            return Notificate("License not found.");

        if (license.Active)
            license.Desactivate();
        else
            license.Activate();

        await _unitOfWork.LicenseRepository.Update(license);

        return true;
    }
}
