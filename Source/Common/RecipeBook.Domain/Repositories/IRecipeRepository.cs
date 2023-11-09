using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IRecipeRepository
{
    Task<Recipe> GetRecipeAsync(Guid id);
    Task<IEnumerable<Recipe>> FindRecipesByAccountIdAsync(Guid id);
    Task CreateRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(Guid id);
}