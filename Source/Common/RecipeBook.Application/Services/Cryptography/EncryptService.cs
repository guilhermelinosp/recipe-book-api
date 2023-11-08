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
        try
        {
            var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes($"{password}{_configuration["EncryptKey"]}");
            var hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string GenerateCode()
    {
        return Guid.NewGuid().ToString("N").Substring(0, 6);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        try
        {
            var result = new StringBuilder();
            foreach (var b in hash) result.Append(b.ToString("X2"));
            return result.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}