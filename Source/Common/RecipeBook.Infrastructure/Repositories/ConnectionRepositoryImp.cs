using Microsoft.EntityFrameworkCore;
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
}