using System.Security.Cryptography;
using System.Text;

namespace Ofernandoavila.Mailman.Business.Utils.Security;

public static class SHA256Criptografy
{
    public static string Encrypt(string password)
    {
        string result = string.Empty;
        var message = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(message);

        foreach(byte b in hash) result += b.ToString("X2");

        return result;
    }
}