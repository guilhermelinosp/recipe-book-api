using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneAsync(string phone);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}