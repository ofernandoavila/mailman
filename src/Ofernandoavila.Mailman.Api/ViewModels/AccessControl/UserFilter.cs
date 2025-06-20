using Ofernandoavila.Mailman.Api.ViewModels.DTO;

namespace Ofernandoavila.Mailman.Api.ViewModels.AccessControl
{
    public class UserFilter : PaginationFilter
    {
        public Guid RoleId { get; set; } = Guid.Empty;
    }
}
