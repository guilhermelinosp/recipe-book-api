using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class AccountRepositoryImp : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepositoryImp(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts!.AsNoTracking().ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) ?? null;
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? null;
    }

    public async Task<Account?> GetByPhoneAsync(string phone)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Phone == phone) ?? null;
    }

    public async Task CreateAsync(Account user)
    {
        await _context.Accounts!.AddAsync(user);
    }

    public Task UpdateAsync(Account user)
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