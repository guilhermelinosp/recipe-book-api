using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class RecipeRepositoryImp : IRecipeRepository
{
    private readonly AppDbContext _context;

    public RecipeRepositoryImp(AppDbContext context)
    {
        _context = context;
    }

    public Task<Recipe?> GetRecipeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Recipe>> FindRecipesByAccountIdAsync(Guid id)
    {
        return await _context.Recipes!
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Where(r => r.AccountId == id)
            .ToListAsync();
    }

    public async Task CreateRecipeAsync(Recipe recipe)
    {
        await _context.Recipes!.AddAsync(recipe);

        await SaveChangesAsync();
    }

    public Task UpdateRecipeAsync(Recipe recipe)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRecipeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}