using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class AccountRepositoryImp : IAccountRepository
{
    private readonly RecipeBookDbContext _context;

    public AccountRepositoryImp(RecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.AccountId == id);
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Account?> GetByCodeAsync(string code)
    {
        return await _context.Accounts!.AsNoTracking().FirstOrDefaultAsync(u => u.Code == code);
    }

    public async Task CreateAsync(Account account)
    {
        await _context.Accounts!.AddAsync(account);

        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts!.Update(account);

        await SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts!.AsNoTracking().ToListAsync();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}