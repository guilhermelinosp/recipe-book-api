#nullable enable
using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task<Account?> GetByEmailAsync(string email);
    Task<Account?> GetByPhoneAsync(string phone);
    Task<Account?> GetByCodeAsync(string code);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Guid id);
    Task CreateAsync(Account account);
}