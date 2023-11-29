using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class RecipeRepositoryImp : IRecipeRepository
{
    private readonly RecipeBookDbContext _context;

    public RecipeRepositoryImp(RecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task<Recipe?> FindRecipeByIdAsync(Guid recipeId, Guid accountId)
    {
        return await _context.Recipes!
            .Include(r => r.Ingredients)
            .Where(r => r.AccountId == accountId)
            .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
    }

    public async Task<IEnumerable<Recipe>?> FindRecipesAsync(Guid accountId)
    {
        return await _context.Recipes!
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Where(r => r.AccountId == accountId)
            .ToListAsync();
    }

    public async Task<Recipe?> FindRecipeByTitleAsync(string title, Guid accountId)
    {
        return await _context.Recipes!
            .AsNoTracking()
            .Where(r => r.AccountId == accountId && r.Title == title)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync();
    }

    public async Task CreateRecipeAsync(Recipe recipe)
    {
        await _context.Recipes!.AddAsync(recipe);

        await SaveChangesAsync();
    }

    public Task UpdateRecipeAsync(Recipe recipe)
    {
        _context.Recipes!.Update(recipe);

        return SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(Recipe recipe)
    {
        _context.Recipes!.Remove(recipe);

        await SaveChangesAsync();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}