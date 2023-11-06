using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.UseCases.Accounts;

public interface IAccountUseCase
{
    Task<Account?> GetMyAccountAsync(string token);
}