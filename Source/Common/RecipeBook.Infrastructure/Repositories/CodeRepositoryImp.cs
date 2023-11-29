using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class CodeRepositoryImp : ICodeRepository
{
    private readonly RecipeBookDbContext _context;

    public CodeRepositoryImp(RecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task CreateCodeAsync(Code code)
    {
        var existingCode = await _context.Codes!.FirstOrDefaultAsync(c => c.AccountId == code.AccountId)!;

        if (existingCode is not null) _context.Codes!.Remove(existingCode);

        await _context.Codes!.AddAsync(code);

        await SaveChangesAsync();
    }

    public Task<Code?> FindCodeByAccountIdAsync(Guid accountId)
    {
        return _context.Codes!.AsNoTracking().FirstOrDefaultAsync(c => c.AccountId == accountId);
    }

    public Task<Code?> FindCodeByCodeValueAsync(string codeValue)
    {
        return _context.Codes!.AsNoTracking().FirstOrDefaultAsync(c => c.CodeValue == codeValue);
    }

    public async Task DeleteCodeAsync(Guid accountId)
    {
        var code = await _context.Codes!.FirstOrDefaultAsync(c => c.AccountId == accountId);

        if (code is not null) _context.Codes!.Remove(code);

        await SaveChangesAsync();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}