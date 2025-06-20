using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ofernandoavila.Mailman.Business.Interfaces.Services.License;

public interface ILicenseService : IDisposable
{
    Task<int> GetTotal(Expression<Func<Models.License.License, bool>> predicate);
    Task<IEnumerable<Models.License.License>> GetAll(int pageNumber, int pageSize, Expression<Func<Models.License.License, bool>> predicate, Expression<Func<Models.License.License, object>> OrderBy, bool desc);
    Task<Models.License.License> GetById(Guid id);
    Task<bool> Add(Models.License.License license);
    Task<bool> Update(Models.License.License license);
    Task<bool> Delete(Guid id);
    Task<bool> UpdateStatus(Guid id);
    Task Complete();
}