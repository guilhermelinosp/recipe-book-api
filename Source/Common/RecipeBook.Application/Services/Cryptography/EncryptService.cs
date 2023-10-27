using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services.Cryptography;

public class EncryptService
{
    private readonly string _securityKey;

    public EncryptService(string securityKey)
    {
        _securityKey = securityKey;
    }

    public string EncryptPassword(string input)
    {
        var key = $"{input}{_securityKey}";

        var sha512 = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(key);
        var hash = sha512.ComputeHash(bytes);
        return GetStringFromHash(hash);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        var result = new StringBuilder();
        foreach (var b in hash)
        {
            result.Append(b.ToString("X2"));
        }

        return result.ToString();
    }
}