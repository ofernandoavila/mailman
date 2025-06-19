using Moq.AutoMock;
using Ofernandoavila.Mailman.Business.Models.Services.Parameter;

namespace Ofernandoavila.Mailman.Business.Tests._Fixture.Parameter
{
    [CollectionDefinition(nameof(RoleCollection))]
    public class RoleCollection : ICollectionFixture<RoleTestsFixture>
    { }
    public class RoleTestsFixture : IDisposable
    {
        public AutoMocker Mocker;
        public RoleService RoleService;

        public RoleService CreateRoleService()
        {
            Mocker = new AutoMocker();
            RoleService = Mocker.CreateInstance<RoleService>();

            return RoleService;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
