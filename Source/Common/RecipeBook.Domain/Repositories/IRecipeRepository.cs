using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IRecipeRepository
{
<<<<<<< HEAD
    Task<Recipe> GetRecipeAsync(Guid id);
    Task<IEnumerable<Recipe>> FindRecipesByAccountIdAsync(Guid id);
=======
    Task<Recipe?> GetRecipeAsync(Guid id);
    Task<IEnumerable<Recipe>> GetRecipesAsync();
>>>>>>> 5e0ad8c7692647508c252071f6c986e6f55a96d0
    Task CreateRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(Guid id);
}