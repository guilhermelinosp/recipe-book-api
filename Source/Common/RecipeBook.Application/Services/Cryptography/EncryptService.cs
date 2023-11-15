using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services.Cryptography;

public class EncryptService : IEncryptService
{
    private readonly IConfiguration _configuration;

    public EncryptService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string EncryptPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes($"{password}{_configuration["EncryptKey"]}");
        var hash = SHA512.HashData(bytes);
        return GetStringFromHash(hash);
    }

    public string GenerateCode()
    {
        return Guid.NewGuid().ToString("N").Substring(0, 6);
    }

    private static string GetStringFromHash(IEnumerable<byte> hash)
    {
            var result = new StringBuilder();
            foreach (var b in hash) result.Append(b.ToString("X2"));
            return result.ToString();
    }
}