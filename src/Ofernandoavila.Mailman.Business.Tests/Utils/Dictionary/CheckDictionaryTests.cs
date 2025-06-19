using FluentAssertions;
using Ofernandoavila.Mailman.Business.Models.Enumerator;
using Ofernandoavila.Mailman.Business.Utils.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofernandoavila.Mailman.Business.Tests.Utils.Dictionary
{
    [Trait("Unit Test", "CheckDictionary")]
    public class CheckDictionaryTests
    {
        [Theory(DisplayName = "CheckDictionary ValidateRole Valid")]
        [InlineData("0b9b96b8-c083-4c5e-b2b3-c9b142302def")]
        [InlineData("775611a5-7f0b-46b9-8a2c-1f2526d865e5")]
        public void CheckDictionary_ValidateRole_MustReturnValid(Guid input)
        {
            var result = CheckDictionary.ValidateRole(input);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "CheckDictionary ValidateRole Invalid")]
        public void CheckDictionary_ValidateRole_MustReturnInvalid()
        {
            var result = CheckDictionary.ValidateRole(Guid.NewGuid());

            result.Should().BeFalse();
        }

        [Theory(DisplayName = "CheckDictionary GetRoleEnum Valid")]
        [InlineData("0b9b96b8-c083-4c5e-b2b3-c9b142302def", (int) RoleEnum.System)]
        [InlineData("775611a5-7f0b-46b9-8a2c-1f2526d865e5", (int) RoleEnum.Developer)]
        public void CheckDictionary_GetRoleEnum_MustReturnValid(Guid input, int roleEnum)
        {
            var result = CheckDictionary.GetRoleEnum(input);

            result.Should().Be(roleEnum);
        }

        [Theory(DisplayName = "CheckDictionary GetRoleEnum Invalid")]
        [InlineData("0b9b96b8-c083-4c5e-b2b3-c9b142302def", (int)RoleEnum.Developer)]
        [InlineData("775611a5-7f0b-46b9-8a2c-1f2526d865e5", (int)RoleEnum.System)]
        public void CheckDictionary_GetRoleEnum_MustReturnInvalid(Guid input, int roleEnum)
        {
            var result = CheckDictionary.GetRoleEnum(input);

            result.Should().NotBe(roleEnum);
        }

        [Theory(DisplayName = "CheckDictionary GetRoleId Valid")]
        [InlineData("0b9b96b8-c083-4c5e-b2b3-c9b142302def", RoleEnum.System)]
        [InlineData("775611a5-7f0b-46b9-8a2c-1f2526d865e5", RoleEnum.Developer)]
        public void CheckDictionary_GetRoleId_MustReturnValid(Guid input, RoleEnum roleEnum)
        {
            var result = CheckDictionary.GetRoleId(roleEnum);

            result.Should().Be(input);
        }

        [Theory(DisplayName = "CheckDictionary GetRoleId Invalid")]
        [InlineData("0b9b96b8-c083-4c5e-b2b3-c9b142302def", RoleEnum.Developer)]
        [InlineData("775611a5-7f0b-46b9-8a2c-1f2526d865e5", RoleEnum.System)]
        public void CheckDictionary_GetRoleId_MustReturnInvalid(Guid input, RoleEnum roleEnum)
        {
            var result = CheckDictionary.GetRoleId(roleEnum);

            result.Should().NotBe(input);
        }
    }
}
