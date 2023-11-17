using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IRecipeRepository
{
    Task<Recipe> FindRecipeByIdAsync(Guid recipeId, Guid accountId);
    Task<IEnumerable<Recipe>> FindRecipesAsync(Guid accountId);
    Task<Recipe> FindRecipeByTitleAsync(string title, Guid accountId);
    Task CreateRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(Recipe recipe);
}