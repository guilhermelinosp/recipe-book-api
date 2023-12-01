using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class ConnectionRepositoryImp : IConnectionRepository
{
    private readonly RecipeBookDbContext _context;

    public ConnectionRepositoryImp(RecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckConnectionAsync(Guid accountId, Guid subAccountId)
    {
        return await _context.Connections!.AnyAsync(c => c.AccountId == accountId && c.SubAccountId == subAccountId);
    }

    public async Task CreateConnectionAsync(Guid accountId, Guid subAccountId)
    {
        await _context.Connections!.AddAsync(new Connection(accountId, subAccountId));
        await SaveChangesAsync();
    }

    public async Task DeleteConnectionAsync(Guid accountId)
    {
        var connection = await FindConnectionByAccountIdAsync(accountId);
        _context.Connections!.Remove(connection!);
        await SaveChangesAsync();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    private async Task<Connection?> FindConnectionByAccountIdAsync(Guid accountId)
    {
        return await _context.Connections!.FirstOrDefaultAsync(c => c.AccountId == accountId);
    }
}