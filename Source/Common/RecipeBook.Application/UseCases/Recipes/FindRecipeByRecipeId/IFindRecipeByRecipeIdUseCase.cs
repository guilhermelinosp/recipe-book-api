using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.UseCases.Recipes.FindRecipeByRecipeId;

public interface IFindRecipeByRecipeIdUseCase
{
    Task<Recipe> FindRecipeByRecipeIdAsync(string token, Guid recipeId);
}