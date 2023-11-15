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

    public async Task<Recipe?> FindRecipeByRecipeIdAsync(Guid recipeId, Guid accountId)
    {
        return await _context.Recipes!
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Where(r => r.AccountId == accountId)
            .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
    }

    public async Task<IEnumerable<Recipe>?> FindRecipesByAccountIdAsync(Guid accountId)
    {
        return await _context.Recipes!
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Where(r => r.AccountId == accountId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> FindRecipesByTitleAsync(string title, Guid accountId)
    {
        return await _context.Recipes!
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Where(r => r.AccountId == accountId)
            .Where(r => r.Title == title)
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