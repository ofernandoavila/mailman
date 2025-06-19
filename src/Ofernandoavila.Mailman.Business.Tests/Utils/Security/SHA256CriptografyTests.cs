using FluentAssertions;
using Ofernandoavila.Mailman.Business.Utils.Security;

namespace Ofernandoavila.Mailman.Business.Tests.Utils.Security
{
    [Trait("Unit Test", "SHA256Criptografy")]
    public class SHA256CritografyTests
    {
        [Theory(DisplayName = "Valid Criptografy")]
        [InlineData("@Aa12345", "12e8f78ad914d3aef2532615a71c541bbed8473a6140399ca54d3ddd516e1ad8")]
        [InlineData("Ck#3103", "1ec60b3d8960df573d109151008af5f731f702d4e03826badca3ac005d6842ef")]
        [InlineData("05768123741", "7DC867F5A80AB4811ED57078A0ADBB988E0C1AF8FA3D17796C9451D42DCAEE7E")]
        [InlineData("123", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")]
        public void SHA256Criptografy_Encrypt_MustReturnValidHash(string input, string shouldBe)
        {
            var result = SHA256Criptografy.Encrypt(input);

            result.ToLower().Should().Be(shouldBe.ToLower());
            result.Should().NotBeNull();
            result.Should().MatchRegex("[A-Fa-f0-9]{64}");
        }

        [Theory(DisplayName = "Invalid Criptografy")]
        [InlineData("@Aa12345", "d0567c103d38a2f1e5b904850e34eb13254f6a2758c5475c9e86658716cb25fc")]
        [InlineData("Ck#3103", "1ec60b3d8960df573d109151009bf5f731f702d4e03826badca3ac005d6842ef")]
        [InlineData("05768123741", "7DC867F5A80AB4811ED57078A0ADBC988E0C1AF8FA3D17796C9451D42DCAEE7E")]
        [InlineData("123", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae4")]
        public void SHA256Criptografy_Encrypt_MustReturnInvalidHash(string input, string shouldBe)
        {
            var result = SHA256Criptografy.Encrypt(input);

            result.Should().NotBe(shouldBe);
            result.Should().NotBeNull();
            result.Should().MatchRegex("[A-Fa-f0-9]{64}");
        }
    }
}
