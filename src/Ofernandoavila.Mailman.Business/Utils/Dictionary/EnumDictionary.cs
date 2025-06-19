using Ofernandoavila.Mailman.Business.Models.Enumerator;

namespace Ofernandoavila.Mailman.Business.Utils.Dictionary;

public static class EnumDictionary
{
    public static Dictionary<int, Guid> RoleDictionary()
    {
        return new Dictionary<int, Guid>()
        {
            { (int)RoleEnum.System, Guid.Parse("0b9b96b8-c083-4c5e-b2b3-c9b142302def") },
            { (int)RoleEnum.Developer, Guid.Parse("775611a5-7f0b-46b9-8a2c-1f2526d865e5") }
        };
    }
}