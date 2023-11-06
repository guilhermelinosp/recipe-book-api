using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task UpdateAsync(Account user);
    Task DeleteAsync(Guid id);
    Task<Account?> GetByEmailAsync(string email);
    Task<Account?> GetByPhoneAsync(string phone);
    Task CreateAsync(Account user);
    Task SaveChangesAsync();
}