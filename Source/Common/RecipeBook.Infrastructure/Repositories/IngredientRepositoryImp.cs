using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;

namespace RecipeBook.Infrastructure.Repositories;

public class IngredientRepositoryImp : IIngredientRepository
{
    private readonly AppDbContext _context;

    public IngredientRepositoryImp(AppDbContext context)
    {
        _context = context;
    }

    public Task<Ingredient?> GetIngredientAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Ingredient>> GetIngredientsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task CreateIngredientAsync(Ingredient ingredient)
    {
        await _context.Ingredients!.AddAsync(ingredient);
        await SaveChangesAsync();
    }

    public Task UpdateIngredientAsync(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIngredientAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}