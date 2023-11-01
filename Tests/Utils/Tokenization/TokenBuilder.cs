using Moq;
using RecipeBook.Application.Services.Tokenization;

namespace Utils.Tokenization;

public class TokenBuilder
{
    private static TokenBuilder? _instance;
    private readonly Mock<TokenService>? _mock;

    private TokenBuilder(string securityKey, double tokenExpiration)
    {
        _mock ??= new Mock<TokenService>(securityKey, tokenExpiration);
    }

    public static TokenBuilder? Instance(string securityKey = "test", double tokenExpiration = 30.0)
    {
        _instance = new TokenBuilder(securityKey, tokenExpiration);
        return _instance;
    }

    public TokenService Build()
    {
        return _mock!.Object;
    }
}