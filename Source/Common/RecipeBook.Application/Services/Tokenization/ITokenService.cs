using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Services.Tokenization;

public interface ITokenService
{
    string GenerateToken(Account user);
    string GenerateRefreshToken();
    Guid ValidateToken(string token);
}