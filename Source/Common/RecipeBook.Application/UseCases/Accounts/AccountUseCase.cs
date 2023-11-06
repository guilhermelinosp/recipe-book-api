using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;

namespace RecipeBook.Application.UseCases.Accounts;

public class AccountUseCase : IAccountUseCase
{
    private readonly ITokenService _token;
    private readonly IAccountRepository _account;

    public AccountUseCase(ITokenService token, IAccountRepository account)
    {
        _token = token;
        _account = account;
    }

    public async Task<Account?> GetMyAccountAsync(string token)
    {
        var id = _token.GetIdFromToken(token);

        return await _account.GetByIdAsync(id) ?? null;
    }
}