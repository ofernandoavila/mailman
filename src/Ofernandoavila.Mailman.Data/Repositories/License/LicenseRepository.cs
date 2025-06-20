using Ofernandoavila.Mailman.Business.Interfaces.Repositories;
using Ofernandoavila.Mailman.Data.Context;

namespace Ofernandoavila.Mailman.Data.Repositories.License;

public class LicenseRepository(AppDbContext context) : Repository<Business.Models.License.License>(context), ILicenseRepository
{
}