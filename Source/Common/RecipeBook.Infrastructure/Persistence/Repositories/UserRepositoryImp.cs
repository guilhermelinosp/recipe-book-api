using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;

namespace RecipeBook.Infrastructure.Persistence.Repositories;

public class UserRepositoryImp : IUserRepository
{
    private readonly InfrastructureDbContext _context;

    public UserRepositoryImp(InfrastructureDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users!.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await _context.Users!.AsNoTracking().FirstOrDefaultAsync(u => u.Phone == phone);
    }


    public async Task CreateAsync(User user)
    {
        Console.WriteLine(user);
        await _context.Users!.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}