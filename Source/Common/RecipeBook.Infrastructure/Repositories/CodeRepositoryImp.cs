using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class CodeRepositoryImp : ICodeRepository
{
    private readonly AppDbContext _context;

    public CodeRepositoryImp(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateCodeAsync(Code code)
    {
        await _context.Codes!.AddAsync(code);
        
        await SaveChangesAsync();
    }

    public  async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}