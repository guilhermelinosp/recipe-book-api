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

    public Task<IEnumerable<Recipe>> GetRecipesAsync()
    {
        throw new NotImplementedException();
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