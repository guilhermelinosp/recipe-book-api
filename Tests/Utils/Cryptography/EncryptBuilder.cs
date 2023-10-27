using Moq;
using RecipeBook.Application.Services.Cryptography;

namespace Utils.Test.Cryptography;

public class EncryptBuilder
{
    private static EncryptBuilder? _instance;
    private readonly Mock<EncryptService>? _mock;

    private EncryptBuilder(string securityKey)
    {
        _mock = new Mock<EncryptService>(securityKey);
    }

    public static EncryptBuilder? Instance(string securityKey = "test")
    {
        _instance = new EncryptBuilder(securityKey);
        return _instance;
    }

    public EncryptService Build()
    {
        return _mock!.Object;
    }
}