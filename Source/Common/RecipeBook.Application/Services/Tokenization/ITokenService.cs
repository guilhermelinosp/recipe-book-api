using Microsoft.AspNetCore.Identity;

namespace RecipeBook.Application.Services.Tokenization;

public interface ITokenService
{
    bool ValidateToken(string token);
    string GenerateToken(IdentityUser user);
    string GenerateRefreshToken();
    string GetEmailFromToken(string token);
    string GetPhoneFromToken(string token);
    Guid GetIdFromToken(string token);

}