using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IRecipeRepository
{
    Task<Recipe> FindRecipeByRecipeIdAsync(Guid recipeId, Guid accountId);
    Task<IEnumerable<Recipe>> FindRecipesByAccountIdAsync(Guid accountId);
    Task<IEnumerable<Recipe>> FindRecipesByTitleAsync(string title, Guid accountId);
    Task CreateRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(Guid id);
}